using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Helper.Extensions;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Stock.Medication.Queries.GetLevel;

public class GetLevelsQueryHandler(IUnitOfWork unitOfWork)
    : BaseHandler<GetLevelsQuery, Result<PaginationResponse<GetLevelsResponse>>>
{
    private readonly IGenericRepository<Domain.Entities.Stock.MedicationStock> _medicationRepo =
        unitOfWork.GetRepository<Domain.Entities.Stock.MedicationStock>();

    public override async Task<Result<PaginationResponse<GetLevelsResponse>>> Handle(
        GetLevelsQuery request,
        CancellationToken cancellationToken
    )
    {
        var query = await _medicationRepo.GetAllQueryableAsync(
            d => !d.Is_Deleted,
            Include: q => q.Include(a => a.Medicine).ThenInclude(m => m.ReorderSettings!)
        );

        if (!string.IsNullOrWhiteSpace(request.MedicineName))
            query = query.Where(a => a.Medicine.Name.Contains(request.MedicineName));

        // Prepare the grouping
        var groupedQuery = query
            .Select(x => new
            {
                x.MedicineId,
                MedicineName = x.Medicine.Name,
                TotalQuantity = x.Quantity,
                ReorderPoint = x.Medicine.ReorderSettings != null
                    ? x.Medicine.ReorderSettings.ReorderPoint
                    : 0,
                RestockingQuantity = x.Medicine.ReorderSettings != null
                    ? x.Medicine.ReorderSettings.RestockingQuantity
                    : 0,
                RecorderPointId = x.Medicine.ReorderSettings != null
                    ? x.Medicine.ReorderSettings.Id
                    : (Guid?)null,
            })
            .GroupBy(x => new
            {
                x.MedicineId,
                x.MedicineName,
                x.ReorderPoint,
                x.RestockingQuantity,
                x.RecorderPointId,
            });

        if (request.IsBelowReorderPoint.HasValue)
        {
            if (request.IsBelowReorderPoint.Value)
            {
                // Filter for medicines whose total quantity is less than or equal to the reorder point.
                groupedQuery = groupedQuery.Where(g =>
                    g.Sum(x => x.TotalQuantity) <= g.Key.ReorderPoint
                );
            }
            else
            {
                // Filter for medicines whose total quantity is greater than the reorder point.
                groupedQuery = groupedQuery.Where(g =>
                    g.Sum(x => x.TotalQuantity) > g.Key.ReorderPoint
                );
            }
        }

        var finalQuery = groupedQuery.Select(g => new GetLevelsResponse
        {
            MedicineId = g.Key.MedicineId,
            MedicineName = g.Key.MedicineName,
            TotalQuantity = g.Sum(x => x.TotalQuantity),
            ReorderPoint = g.Key.ReorderPoint,
            RestockingQuantity = g.Key.RestockingQuantity,
            IsBelowReorderPoint = g.Sum(x => x.TotalQuantity) <= g.Key.ReorderPoint,
            RecorderPointId = g.Key.RecorderPointId,
        });

        var count = await finalQuery.CountAsync(cancellationToken);
        var response = finalQuery.Paginate(request).ToList();

        return Result<PaginationResponse<GetLevelsResponse>>.Success(
            new PaginationResponse<GetLevelsResponse> { Data = response, Count = count }
        );
    }
}
