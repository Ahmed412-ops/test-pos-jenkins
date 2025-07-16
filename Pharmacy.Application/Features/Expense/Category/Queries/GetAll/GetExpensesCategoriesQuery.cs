using MediatR;
using Pharmacy.Application.Dto;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Expense.Category.Queries.GetAll;

public class GetExpensesCategoriesQuery : Pagination, IRequest<Result<PaginationResponse<GetExpensesCategoriesResponse>>>
{
    public string? Name { get; set; }
}
