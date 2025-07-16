using Pharmacy.Domain.Entities.Wallets.Expense;
using Pharmacy.Domain.Entities.Wallets.Sales;

namespace Pharmacy.Domain.Entities.Wallets;

public class ShiftWallet : BaseEntity
{
    public Guid ShiftId { get; set; }
    public Shift Shift { get; set; } = null!;
    public Guid WalletId { get; set; }
    public Wallet Wallet { get; set; } = null!;
    public required decimal OpeningBalance { get; set; }
    public decimal TotalSales => PrescriptionTransactions.Sum(x => x.AmountPaid);
    public decimal TotalExpenses =>
        CashExpenses.Sum(x => x.Amount)
        + SupplierTransactions.Sum(x => x.Amount)
        + ReturnTransactions.Sum(x => x.TotalRefunded);
    public decimal ExpectedClosingAmount => OpeningBalance + TotalSales - TotalExpenses;
    public decimal? ActualClosingBalance { get; set; }
    public decimal? Difference => ActualClosingBalance - ExpectedClosingAmount;
    public string? DifferenceReason { get; set; }
    public ICollection<PrescriptionTransaction> PrescriptionTransactions { get; set; } = [];
    public ICollection<CashExpense> CashExpenses { get; set; } = [];
    public ICollection<SupplierTransaction.SupplierTransaction> SupplierTransactions { get; set; } =
        [];
    public ICollection<Return.Return> ReturnTransactions { get; set; } = [];
}
