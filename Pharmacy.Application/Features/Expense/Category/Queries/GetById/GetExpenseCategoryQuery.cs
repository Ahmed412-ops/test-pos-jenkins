using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Expense.Category.Queries.GetById;

public record GetExpenseCategoryQuery(Guid Id) : IRequest<Result<GetExpenseCategoryResponse>>;
