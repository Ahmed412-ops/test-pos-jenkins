using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Shifts.CashExpenses.Queries.GetById;

public record GetCashExpenseQuery(Guid Id) : IRequest<Result<GetCashExpenseResponse>>;

