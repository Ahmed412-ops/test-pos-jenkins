using MediatR;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Expense.Category.Queries.DropDown;

public class ExpenseCategoryDropDownQuery : IRequest<Result<List<DropDownQueryResponse>>>;
