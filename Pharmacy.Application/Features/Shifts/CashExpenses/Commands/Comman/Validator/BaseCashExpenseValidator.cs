using FluentValidation;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Features.Shifts.CashExpenses.Commands.Comman.Interfaces;
using Pharmacy.Application.Resources.Static;

namespace Pharmacy.Application.Features.Shifts.CashExpenses.Commands.Comman.Validator
{
    public abstract class BaseCashExpenseValidator<T> : AbstractValidator<T> where T : ICashExpenseCommand
    {
        protected BaseCashExpenseValidator(IUnitOfWork unitOfWork)
        {
            var categoryRepository = unitOfWork.GetRepository<Domain.Entities.Wallets.Expense.ExpenseCategory>();
            var walletRepository = unitOfWork.GetRepository<Domain.Entities.Wallets.Wallet>();

            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage(Messages.AmountMustBeGreaterThanZero);

            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .WithMessage(Messages.ExpenseCategoryIdRequired)
                .MustAsync(async (id, _) =>
                    await categoryRepository.IsExistsAsync(x => x.Id == id))
                .WithMessage(Messages.ExpenseCategoryNotFound);


            RuleFor(x => x.WalletId)
                .NotEmpty()
                .WithMessage(Messages.WalletIdRequired)
                 .MustAsync(async (id, _) =>
                    await walletRepository.IsExistsAsync(x => x.Id == id))
                .WithMessage(Messages.WalletNotFoundInThisShift);
        }
    }

}
