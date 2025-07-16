using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Commands;

namespace Pharmacy.Application.Features.Expense.Category.Commands.Update;

public class UpdateExpenseCategoryCommandValidator : UpdateBaseCommandValidator<UpdateExpenseCategoryCommand, Domain.Entities.Wallets.Expense.ExpenseCategory>
{
    public UpdateExpenseCategoryCommandValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
