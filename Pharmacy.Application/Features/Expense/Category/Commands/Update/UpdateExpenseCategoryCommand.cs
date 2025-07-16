using Pharmacy.Application.Dto.Common.Commands;
using Pharmacy.Application.Features.Expense.Category.Commands.Create;

namespace Pharmacy.Application.Features.Expense.Category.Commands.Update;

public class UpdateExpenseCategoryCommand : CreateExpenseCategoryCommand, IBaseUpdateCommand
{
    public Guid Id { get; set; }
}
