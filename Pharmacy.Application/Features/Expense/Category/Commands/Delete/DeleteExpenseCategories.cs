using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Expense.Category.Commands.Delete;

public record DeleteExpenseCategoryCommand(Guid Id) : IRequest<Result<bool>>;

