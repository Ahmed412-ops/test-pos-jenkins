namespace Pharmacy.Domain.Entities.Stock;

public class RecorderPointSettings : BaseEntity
{
    public Guid MedicineId { get; set; }
    public Medicine.Medicine Medicine { get; set; } = null!;
    public Guid? PreferredSupplierId { get; set; }
    public Supplier.Supplier? PreferredSupplier { get; set; }
    public decimal ReorderPoint { get; set; }
    public decimal RestockingQuantity { get; set; }
    public bool NotificationsEnabled { get; set; } = true;
}
