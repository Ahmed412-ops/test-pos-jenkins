namespace Pharmacy.Domain.Entities.Wallets.Expense;

public class CashExpense : BaseEntity
{
    public Guid CategoryId { get; set; }
    public required ExpenseCategory Category { get; set; }
    public Guid ShiftWalletId { get; set; }
    public required ShiftWallet ShiftWallet { get; set; } 
    public decimal Amount { get; set; }
    public DateTime ExpenseDateTime { get; set; } = DateTime.UtcNow;
    public string? PaidTo { get; set; }
    public string? Notes { get; set; }
}
