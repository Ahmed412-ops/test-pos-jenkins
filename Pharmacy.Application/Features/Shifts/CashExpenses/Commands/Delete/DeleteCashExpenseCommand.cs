using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Shifts.CashExpenses.Commands.Delete;

public record DeleteCashExpenseCommand(Guid Id) : IRequest<Result<bool>>;

