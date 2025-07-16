using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Medicine.Medicine.Commands.Update;

public class UpdateMedicineCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<UpdateMedicineCommand, Result<string>>
{
    private readonly IGenericRepository<Domain.Entities.Medicine.Medicine> _medicineRepository =
        unitOfWork.GetRepository<Domain.Entities.Medicine.Medicine>();

    public override async Task<Result<string>> Handle(
        UpdateMedicineCommand request,
        CancellationToken cancellationToken
    )
    {
        // 1) Load with AsNoTracking + split queries
        var medicine = await _medicineRepository.FindAsync(
            m => m.Id == request.Id,
            Include: m =>
                m.Include(x => x.MedicineCategory)
                    .Include(x => x.Manufacturer)
                    .Include(x => x.DosageForm)
                    .Include(x => x.MedicineUnits)
                    .ThenInclude(mu => mu.Unit)
                    .Include(x => x.EffectiveMaterials)
                    .ThenInclude(em => em.EffectiveMaterial!)
                    .Include(x => x.CrossSellingRecommendations),
            asSplit: true
        );

        if (medicine == null)
            return Result<string>.Fail(Messages.MedicineNotFound);

        medicine.MedicineUnits.Clear();
        medicine.EffectiveMaterials.Clear();
        medicine.CrossSellingRecommendations.Clear();

        mapper.Map(request, medicine);

        await unitOfWork.SaveChangesAsync();

        return Result<string>.Success(Messages.SuccessfullyUpdated);
    }
}
