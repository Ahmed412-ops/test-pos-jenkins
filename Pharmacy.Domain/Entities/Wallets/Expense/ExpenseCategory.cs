namespace Pharmacy.Domain.Entities.Wallets.Expense;

public class ExpenseCategory : EntityModel
{
    public bool IsActive { get; set; } = true;
    public ICollection<CashExpense> CashExpenses { get; set; } = [];
}
