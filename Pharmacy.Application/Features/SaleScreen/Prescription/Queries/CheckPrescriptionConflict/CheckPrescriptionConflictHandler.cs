using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Application.Services.Abstraction.MedicineConflictService;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.SaleScreen.Prescription.Queries.CheckPrescriptionConflict;

public class CheckPrescriptionConflictHandler(
    IUnitOfWork unitOfWork,
    IConflictChecker conflictChecker
) : BaseHandler<CheckMedicineConflictQuery, Result<CheckPrescriptionConflictResponse>>
{
    private readonly IGenericRepository<Domain.Entities.Customers.Customer> _customerRepo =
        unitOfWork.GetRepository<Domain.Entities.Customers.Customer>();

    private readonly IGenericRepository<Domain.Entities.Medicine.Medicine> _medicineRepo =
        unitOfWork.GetRepository<Domain.Entities.Medicine.Medicine>();

    public override async Task<Result<CheckPrescriptionConflictResponse>> Handle(
        CheckMedicineConflictQuery request,
        CancellationToken cancellationToken
    )
    {
        var medicineExists = await _medicineRepo.IsExistsAsync(m => m.Id == request.NewMedicineId);
        if (!medicineExists)
            return Result<CheckPrescriptionConflictResponse>.Fail(
                message: Messages.MedicineNotFound
            );

        var medicineIds = request.ExistingMedicineIds?.AsEnumerable() ?? [];
        var customerDiseasesIds = new List<Guid>();

        if (request.CustomerId != null)
        {
            var chronicMedicine = await _customerRepo.FindAsync(
                c => c.Id == request.CustomerId,
                select: c => new
                {
                    ChronicMedicineIds = c
                        .CustomerChronicMedicines.Select(m => m.MedicineId)
                        .ToList(),
                    ChronicDiseases = c
                        .CustomerChronicDiseases.Select(d => d.DiseaseId)
                        .ToList(),
                }
            );

            medicineIds = medicineIds
                .Union(chronicMedicine?.ChronicMedicineIds ?? Enumerable.Empty<Guid>())
                .Distinct()
                .ToList();
            customerDiseasesIds = chronicMedicine?.ChronicDiseases ?? [];
        }

        var conflicts = await conflictChecker.CheckPrescriptionConflictsAsync(
            medicineIds.ToList(),
            request.NewMedicineId
        );

        return Result<CheckPrescriptionConflictResponse>.Success(
            data: new(conflicts.Count != 0, conflicts),
            message: conflicts.Count != 0
                ? Messages.PrescriptionConflictDetected
                : Messages.PrescriptionConflictFree
        );
    }
}
