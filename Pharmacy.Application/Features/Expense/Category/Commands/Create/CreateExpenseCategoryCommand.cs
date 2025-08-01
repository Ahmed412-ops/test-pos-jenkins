using MediatR;
using Pharmacy.Application.Dto.Common.Commands;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Expense.Category.Commands.Create;

public class CreateExpenseCategoryCommand : IBaseCommand, IRequest<Result<string>>
{
    public required string Name { get ; set ; }
    public string? Description { get ; set ; }
    public string? Notes { get ; set ; }
}

