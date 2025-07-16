using Pharmacy.Domain.Entities.Order;
using Pharmacy.Domain.Enum;

namespace Pharmacy.Domain.Entities.Supplier;

public class Supplier : EntityModel
{
    public SupplierType SupplierType { get; set; }
    public string? Address { get; set; }
    public string? PaymentTerms { get; set; }
    public ICollection<Contact> Contacts { get; set; } = [];
    public ICollection<PurchaseOrder> PurchaseOrders { get; set; } = [];
    public ICollection<SupplierInvoice.SupplierInvoice> SupplierInvoices { get; set; } = [];
}
