using Pharmacy.Application.Dto.Common.Commands;
using Pharmacy.Application.Features.Shifts.CashExpenses.Commands.Create;

namespace Pharmacy.Application.Features.Shifts.CashExpenses.Commands.Update;

public class UpdateCashExpenseCommand : CreateCashExpenseCommand
{
    public Guid Id { get; set; }
}
