using Pharmacy.Domain.Entities.Wallets;
using Pharmacy.Domain.Enum;

namespace Pharmacy.Domain.Entities.SupplierTransaction;

public class SupplierTransaction : BaseEntity
{
    public Guid? SupplierInvoiceId { get; set; }
    public SupplierInvoice.SupplierInvoice? SupplierInvoice { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public PaymentMethod? PaymentMethod { get; set; }
    public string? Notes { get; set; }

    public Guid? ShiftWalletId { get; set; }
    public ShiftWallet? ShiftWallet { get; set; }
}
