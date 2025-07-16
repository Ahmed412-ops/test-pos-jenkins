using Pharmacy.Domain.Entities.Medicine;

namespace Pharmacy.Domain.Entities.SupplierInvoice;

public class SupplierInvoiceItem : BaseEntity
{
    public Guid SupplierInvoiceId { get; set; }
    public SupplierInvoice SupplierInvoice { get; set; } = null!;
    public Guid MedicineUnitId { get; set; }
    public MedicineUnit MedicineUnit { get; set; } = null!;
    public decimal Quantity { get; set; }
    // the reviewed (manually counted) quantity.
    public decimal? ReviewedQuantity { get; set; }
    public decimal QuantityDifference => (ReviewedQuantity ?? Quantity) - Quantity;
    // Pricing details
    public required decimal PublicSellingPrice { get; set; }  // Retail price
    public decimal SupplierDiscountPercentage { get; set; } = 0;
    public decimal PharmacistFixedMargin { get; set; } = 0;
    public decimal DistributorFixedMargin { get; set; } = 0;
    public decimal TaxPercentage { get; set; }
    public decimal SupplierPurchasePrice { get; set; }
    public decimal TotalPurchasePrice { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal FinalPrice { get; set; }
    public required DateOnly ExpiryDate { get; set; }
}
