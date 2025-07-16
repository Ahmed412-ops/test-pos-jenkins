namespace Pharmacy.Domain.Entities.Stock;

public class MedicationStock : BaseEntity
{
    public Guid MedicineId { get; set; }
    public Medicine.Medicine Medicine { get; set; } = null!;
    public decimal Quantity { get; set; }
    public DateOnly ExpiryDate { get; set; }
    public decimal? PurchasePrice { get; set; }
    public decimal SellingPrice { get; set; }
    public string GeneratedBarcode { get; set; } = string.Empty;
}
