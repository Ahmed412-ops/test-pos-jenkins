using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Medicine.Medicine.Queries.GetMedicineUnits;

public class GetMedicineUnitsQueryHandler(IUnitOfWork unitOfWork)
    : BaseHandler<GetMedicineUnitsQuery, Result<List<GetMedicineUnitsResponse>>>
{
    private readonly IGenericRepository<Domain.Entities.Medicine.MedicineUnit> _medicineUnitRepo = unitOfWork.GetRepository<Domain.Entities.Medicine.MedicineUnit>();

    public override async Task<Result<List<GetMedicineUnitsResponse>>> Handle(
        GetMedicineUnitsQuery request,
        CancellationToken cancellationToken)
    {
         var medicineUnits = await _medicineUnitRepo.GetAllAsync(
             d => !d.Is_Deleted,
             Include: query => query.Include(mu => mu.Medicine)
                                    .Include(mu => mu.Unit)
         );

         var grouped = medicineUnits
            .GroupBy(mu => mu.Medicine.Name)
            .Select(g => new GetMedicineUnitsResponse
            {
                MedicineName = g.Key,
                Units = [.. g.Select(mu => new UnitResponse
                {
                    MedicineUnitId = mu.Id,
                    UnitName = mu.Unit.Name
                })]
            })
            .ToList();

         return Result<List<GetMedicineUnitsResponse>>.Success(grouped);
    }
}

