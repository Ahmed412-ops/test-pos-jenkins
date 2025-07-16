using Pharmacy.Domain.Entities.Medicine;

namespace Pharmacy.Domain.Entities.Order;

public class PurchaseOrderItem : BaseEntity
{
    public Guid PurchaseOrderId { get; set; }
    public  PurchaseOrder PurchaseOrder { get; set; } = null!;
    public Guid MedicineUnitId { get; set; }
    public MedicineUnit MedicineUnit { get; set; } = null!;
    public decimal Quantity { get; set; } // always save in the calc unit 
}
