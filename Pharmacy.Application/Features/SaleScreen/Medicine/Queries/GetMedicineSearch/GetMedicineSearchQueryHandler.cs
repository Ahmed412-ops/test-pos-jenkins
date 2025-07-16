using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Helper.Extensions;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.SaleScreen.Medicine.Queries.GetMedicineSearch;

public class GetMedicineSearchQueryHandler(IUnitOfWork unitOfWork)
    : BaseHandler<GetMedicineSearchQuery, Result<PaginationResponse<GetMedicineSearchResponse>>>
{
    private readonly IGenericRepository<Domain.Entities.Stock.MedicationStock> _medicationRepo =
        unitOfWork.GetRepository<Domain.Entities.Stock.MedicationStock>();

    public override async Task<Result<PaginationResponse<GetMedicineSearchResponse>>> Handle(
        GetMedicineSearchQuery request,
        CancellationToken cancellationToken
    )
    {
        var query = await _medicationRepo.GetAllQueryableAsync(
            d => !d.Is_Deleted,
            Include: q => q.Include(a => a.Medicine)
        );

        if (!string.IsNullOrWhiteSpace(request.MedicineName))
            query = query.Where(s => s.Medicine.Name.Contains(request.MedicineName));

        var groupedQuery = query
            .GroupBy(s => new { s.Medicine.Id, s.Medicine.Name })
            .Select(g => new GetMedicineSearchResponse
            {
                MedicineId = g.Key.Id,
                MedicineName = g.Key.Name,
                TotalQuantity = g.Sum(s => s.Quantity),
                HighestPrice = g.Max(s => s.SellingPrice),
            });

        var totalCount = await groupedQuery.CountAsync(cancellationToken);

        var paginatedData = await groupedQuery.Paginate(request).ToListAsync(cancellationToken);

        return Result<PaginationResponse<GetMedicineSearchResponse>>.Success(
            new PaginationResponse<GetMedicineSearchResponse>
            {
                Data = paginatedData,
                Count = totalCount,
            }
        );
    }
}
