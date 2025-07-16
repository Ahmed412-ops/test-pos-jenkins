using Pharmacy.Domain.Entities.Medicine;
using Pharmacy.Domain.Entities.Stock;

namespace Pharmacy.Domain.Entities.Wallets.Sales;

public class PrescriptionItem : BaseEntity
{
    public Guid PrescriptionId { get; set; }
    public Prescription Prescription { get; set; } = null!;
    public Guid MedicationStockId { get; set; }
    public MedicationStock MedicationStock { get; set; } = null!;
    public Guid MedicineUnitId { get; set; }
    public MedicineUnit MedicineUnit { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; } // Selling Price per unit
    public decimal AppliedDiscount { get; set; } // Discount applied to the unit price
    public decimal TotalPrice => (UnitPrice - AppliedDiscount) * Quantity; // Total price after discount
    public int ReturnedQuantity { get; set; } = 0;
    public int AvailableForReturn => Quantity - ReturnedQuantity;
}
