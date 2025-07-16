using Pharmacy.Domain.Entities.Identity;
using Pharmacy.Domain.Enum;

namespace Pharmacy.Domain.Entities.Wallets.Sales;

public class Prescription : BaseEntity
{
    public required int InvoiceNumber { get; set; } // generated // index
    public Guid ShiftId { get; set; }
    public Shift Shift { get; set; } = null!;
    public Guid? CustomerId { get; set; }
    public Customers.Customer? Customer { get; set; }
    public decimal CashbackEarned { get; set; } = 0m;
    public decimal Amount => Items.Sum(x => x.UnitPrice * x.Quantity);
    public decimal Discount => Items.Sum(x => x.AppliedDiscount * x.Quantity);
    public decimal AmountPaid => Transactions.Sum(x => x.AmountPaid);
    public decimal CashbackUsed { get; set; } = 0m;
    public decimal CreditUsed { get; set; } = 0m;
    public decimal AmountDue =>
    TransferStatus == PrescriptionTransferStatus.Transferred ||
    TransferStatus ==PrescriptionTransferStatus.Rejected
        ? 0
        : Amount - AmountPaid - CreditUsed - CashbackUsed - Discount;
    public string? Notes { get; set; }
    public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
    public PrescriptionTransferStatus TransferStatus { get; set; }
     = PrescriptionTransferStatus.None;
    public Guid? TransferredByUserId { get; set; }
    public ApplicationUser? TransferredByUser { get; set; }
    public DateTime? TransferredAt { get; set; }
    public ICollection<PrescriptionItem> Items { get; set; } = [];
    public ICollection<PrescriptionTransaction> Transactions { get; set; } = [];
    public ICollection<Return.Return> Returns { get; set; } = [];
}

public enum PaymentStatus
{
    Pending,
    PartiallyPaid,
    FullyPaid,
}
