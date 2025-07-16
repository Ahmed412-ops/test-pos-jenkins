using Pharmacy.Domain.Entities.Order;
using Pharmacy.Domain.Enum;

namespace Pharmacy.Domain.Entities.SupplierInvoice;

public class SupplierInvoice : BaseEntity
{
    public required string InvoiceNumber { get; set; } // unique
    public Guid SupplierId { get; set; }
    public Supplier.Supplier Supplier { get; set; } = null!;  
    public Guid? PurchaseOrderId { get; set; }  
    public PurchaseOrder? PurchaseOrder { get; set; }  
    public DateTime InvoiceDate { get; set; }
    public DateTime? DueDate { get; set; }
    public decimal ShippingFees { get; set; } = 0;
    public decimal FinalInvoiceTotal { get; set; }
    public decimal AmountPaid { get; set; }
    public decimal RemainingBalance => FinalInvoiceTotal - AmountPaid;
    public PaymentStatus PaymentStatus { get; set; }
    // If PaymentStatus is Paid (or partially paid), these become required.
    public DateTime? PaymentDate { get; set; }
    public PaymentMethod? PaymentMethod { get; set; }
    public string? InvoiceAttachmentUrl { get; set; }
    public string? Notes { get; set; }
    public bool IsRecevied { get; set; } = false;
    public bool IsReviewed { get; set; } = false; // Indicates if the invoice was reviewed
    public ICollection<SupplierInvoiceItem> InvoiceItems { get; set; } = [];
    public ICollection<SupplierTransaction.SupplierTransaction> SupplierTransactions { get; set; } = [];
}
