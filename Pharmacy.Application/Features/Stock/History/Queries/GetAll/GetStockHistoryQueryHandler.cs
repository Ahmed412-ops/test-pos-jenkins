using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Helper.Extensions;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Stock.History.Queries.GetAll;

public class GetStockHistoryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<GetStockHistoryQuery, Result<PaginationResponse<GetStockHistoryResponse>>>
{
    private readonly IGenericRepository<Domain.Entities.Stock.StockHistory> _stockHistoryRepo =
        unitOfWork.GetRepository<Domain.Entities.Stock.StockHistory>();

    public override async Task<Result<PaginationResponse<GetStockHistoryResponse>>> Handle(
        GetStockHistoryQuery request,
        CancellationToken cancellationToken
    )
    {
        var query = await _stockHistoryRepo.GetAllQueryableAsync(
            d => !d.Is_Deleted,
            Include: a => a.Include(b => b.Medicine).Include(b => b.PerformedBy)
        );

        if (!string.IsNullOrWhiteSpace(request.MedicineName))
            query = query.Where(a => a.Medicine.Name == request.MedicineName);

        if (request.FromDate.HasValue)
            query = query.Where(a => a.TransactionDate >= request.FromDate);

        if (request.ToDate.HasValue)
            query = query.Where(a => a.TransactionDate <= request.ToDate);

        if (!string.IsNullOrWhiteSpace(request.PerformedBy))
            query = query.Where(a => a.PerformedBy.UserName == request.PerformedBy);

        if (request.TransactionType.HasValue)
            query = query.Where(a => a.TransactionType == request.TransactionType.Value);

        if (!string.IsNullOrWhiteSpace(request.TransactionReference))
            query = query.Where(a => a.TransactionReference == request.TransactionReference);

        var count = await query.CountAsync(cancellationToken);
        var response = query
            .Select(a => mapper.Map<GetStockHistoryResponse>(a))
            .Paginate(request)
            .ToList();

        return Result<PaginationResponse<GetStockHistoryResponse>>.Success(
            new PaginationResponse<GetStockHistoryResponse> { Data = response, Count = count }
        );
    }
}
