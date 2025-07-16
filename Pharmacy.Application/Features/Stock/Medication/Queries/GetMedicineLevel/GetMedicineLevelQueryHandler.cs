using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Stock.Medication.Queries.GetMedicineLevel;

public class GetMedicineLevelQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
 : BaseHandler<GetMedicineLevelQuery, Result<List<GetMedicineLevelResponse>>>
{
    private readonly IGenericRepository<Domain.Entities.Stock.MedicationStock> _medicationRepo = unitOfWork.GetRepository<Domain.Entities.Stock.MedicationStock>();
    public override async Task<Result<List<GetMedicineLevelResponse>>>
        Handle(GetMedicineLevelQuery request, CancellationToken cancellationToken)
    {
        var query = await _medicationRepo.GetAllQueryableAsync(
            d => !d.Is_Deleted && d.MedicineId == request.MedicineId,
            Include: q => q.Include(a => a.Medicine)
        );

        var response = mapper.Map<List<GetMedicineLevelResponse>>(query.ToList());

        return Result<List<GetMedicineLevelResponse>>.Success(response);
    }
}
