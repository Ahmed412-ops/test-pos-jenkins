using Pharmacy.Domain.Entities.Wallets.Sales;

namespace Pharmacy.Domain.Entities.Customers;

public class Customer : EntityModel
{
    public DateTime? DateOfBirth { get; set; }
    public bool IsChild =>
        DateOfBirth.HasValue
        && (
            DateTime.Today.Year
            - DateOfBirth.Value.Year
            - (DateTime.Today.DayOfYear < DateOfBirth.Value.DayOfYear ? 1 : 0)
        ) < 12;
    public bool EnableContactOption { get; set; }
    public decimal AmountDue => Prescriptions.Sum(p => Math.Max(p.AmountDue, 0m));
    public decimal CashbackBalance =>
        BalanceTransactions
            .Where(t => t.Type == CustomerTransactionType.CashbackEarned)
            .Sum(t => t.Amount)
        - BalanceTransactions
            .Where(t => t.Type == CustomerTransactionType.CashbackUsed)
            .Sum(t => t.Amount);
    public decimal CreditBalance =>
        BalanceTransactions
            .Where(tx =>
                tx.Type == CustomerTransactionType.Overpayment
                || tx.Type == CustomerTransactionType.ManualAdjustment
            )
            .Sum(tx => tx.Amount)
        - BalanceTransactions
            .Where(tx =>
                tx.Type == CustomerTransactionType.CreditUsed
                || tx.Type == CustomerTransactionType.DebtPayment
            )
            .Sum(tx => tx.Amount);
    public ICollection<Address> Addresses { get; set; } = [];
    public ICollection<PhoneNumber> PhoneNumbers { get; set; } = [];
    public ICollection<CustomerChronicMedicine> CustomerChronicMedicines { get; set; } = [];
    public ICollection<CustomerChronicDisease> CustomerChronicDiseases { get; set; } = [];
    public ICollection<Prescription> Prescriptions { get; set; } = [];
    public ICollection<BalanceTransaction> BalanceTransactions { get; set; } = [];
}
