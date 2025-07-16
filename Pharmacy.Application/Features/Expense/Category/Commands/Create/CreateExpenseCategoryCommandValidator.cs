using FluentValidation;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Commands;

namespace Pharmacy.Application.Features.Expense.Category.Commands.Create;

public class CreateExpenseCategoryCommandValidator : BaseCommandValidator<CreateExpenseCategoryCommand, Domain.Entities.Wallets.Expense.ExpenseCategory>
{
    public CreateExpenseCategoryCommandValidator(IUnitOfWork unitOfWork)
        : base(unitOfWork)
    {
    }
}

