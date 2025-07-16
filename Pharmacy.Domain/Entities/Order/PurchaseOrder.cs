using Pharmacy.Domain.Enum;

namespace Pharmacy.Domain.Entities.Order;

public class PurchaseOrder : EntityModel
{
    public required string PurchaseOrderNumber { get; set; }
    public Guid SupplierId { get; set; }
    public required Supplier.Supplier Supplier { get; set; } 
    public required DateTime OrderDate { get; set; }
    public DateTime? ExpectedDeliveryDate { get; set; }
    public PaymentTerms PaymentTerms { get; set; } = PaymentTerms.Cash;
    public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
    public ICollection<PurchaseOrderItem> Items { get; set; } = [];
}
