using MediatR;
using Pharmacy.Application.Dto;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Enum;

namespace Pharmacy.Application.Features.Stock.History.Queries.GetAll;

public class GetStockHistoryQuery : Pagination, IRequest<Result<PaginationResponse<GetStockHistoryResponse>>>
{
    public string? MedicineName { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public string? PerformedBy { get; set; }
    public StockTransactionType? TransactionType { get; set; }
    public string? TransactionReference { get; set; }
}
