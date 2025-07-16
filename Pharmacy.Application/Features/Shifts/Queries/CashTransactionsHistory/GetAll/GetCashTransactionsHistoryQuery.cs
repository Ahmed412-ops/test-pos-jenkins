using MediatR;
using Pharmacy.Application.Dto;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Enum;

namespace Pharmacy.Application.Features.Shifts.Queries.CashTransactionsHistory.GetAll;

public class GetCashTransactionsHistoryQuery : Pagination, IRequest<Result<PaginationResponse<GetCashTransactionsHistoryResponse>>>
{
    public TransactionType? TransactionType { get; set; }
    public string? WalletRegister { get; set; }
    public string? ExpenseCategory { get; set; }
    public string? PerformedBy { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}
