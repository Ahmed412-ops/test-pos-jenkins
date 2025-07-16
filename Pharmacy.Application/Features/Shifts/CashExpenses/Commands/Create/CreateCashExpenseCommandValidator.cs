using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Features.Shifts.CashExpenses.Commands.Comman.Validator;

namespace Pharmacy.Application.Features.Shifts.CashExpenses.Commands.Create;

public class CreateCashExpenseCommandValidator : BaseCashExpenseValidator<CreateCashExpenseCommand>
{
    public CreateCashExpenseCommandValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

}
