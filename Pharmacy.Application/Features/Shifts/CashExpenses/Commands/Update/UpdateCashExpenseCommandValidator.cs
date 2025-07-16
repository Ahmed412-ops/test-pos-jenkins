using FluentValidation;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Features.Shifts.CashExpenses.Commands.Comman.Validator;
using Pharmacy.Application.Resources.Static;

namespace Pharmacy.Application.Features.Shifts.CashExpenses.Commands.Update;

public class UpdateCashExpenseCommandValidator : BaseCashExpenseValidator<UpdateCashExpenseCommand>
{
  public UpdateCashExpenseCommandValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(Messages.IdIsRequired);

    }
}
