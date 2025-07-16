using Pharmacy.Domain.Entities.Customers;

namespace Pharmacy.Domain.Entities.Wallets.Sales;

public class BalanceTransaction : BaseEntity
{
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    public Guid? PrescriptionId { get; set; }
    public Prescription? RelatedPrescription { get; set; }
    public CustomerTransactionType Type { get; set; }
    public decimal Amount { get; set; }
}

public enum CustomerTransactionType
{
    Overpayment,
    CashbackEarned,
    CashbackUsed,
    CreditUsed,
    ManualAdjustment, // for example, when customer paid into his balance directly
    DebtPayment,
    NormalPayment,
    AutoPayment
}
