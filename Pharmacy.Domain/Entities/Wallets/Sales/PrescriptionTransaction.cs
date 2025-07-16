namespace Pharmacy.Domain.Entities.Wallets.Sales;

public class PrescriptionTransaction : BaseEntity
{
    public int InvoiceNumber { get; set; } // generated // index
    public Guid PrescriptionId { get; set; }
    public Prescription Prescription { get; set; } = null!;
    public Guid ShiftWalletId { get; set; }
    public ShiftWallet ShiftWallet { get; set; } = null!;
    public decimal AmountPaid { get; set; }
}
