namespace Pharmacy.Application.Features.Shifts.CashExpenses.Commands.Comman.Interfaces;

public interface ICashExpenseCommand
{
    decimal Amount { get; }
    Guid CategoryId { get; }
    Guid WalletId { get; }
}
