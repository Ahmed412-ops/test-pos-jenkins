using MediatR;
using Pharmacy.Application.Dto;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Shifts.CashExpenses.Queries.GetAll;

public class GetCashExpensesQuery : Pagination, IRequest<Result<PaginationResponse<GetCashExpensesResponse>>>
{
    public DateOnly DateFrom { get; set; }
    public DateOnly DateTo { get; set; }
    public string? Full_Name { get; set; }
    public bool? IsOpen { get; set; }
    public string? CategoryName { get; set; }
}
