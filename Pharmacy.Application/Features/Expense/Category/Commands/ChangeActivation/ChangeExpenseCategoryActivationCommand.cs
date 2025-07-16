using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Expense.Category.Commands.ChangeActivation;

public record ChangeExpenseCategoryActivationCommand(Guid CategoryId) : IRequest<Result<bool>>;

