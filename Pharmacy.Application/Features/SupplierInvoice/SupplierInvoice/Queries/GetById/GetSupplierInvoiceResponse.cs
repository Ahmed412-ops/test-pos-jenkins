namespace Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Queries.GetById;

public class GetSupplierInvoiceResponse
{
    public required string InvoiceNumber { get; set; }
    public Guid SupplierId { get; set; }
    public required string SupplierName { get; set; }
    public Guid? PurchaseOrderId { get; set; }
    public string? PurchaseOrderNumber { get; set; }
    public DateTime InvoiceDate { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime? PaymentDate { get; set; }
    public string? PaymentStatus { get; set; }
    public decimal ShippingFees { get; set; }
    public decimal Subtotal { get; set; }
    public decimal TotalDiscount { get; set; }
    public decimal TotalTaxAmount { get; set; }
    public decimal FinalInvoiceTotal { get; set; }
    public decimal AmountPaid { get; set; }
    public decimal RemainingBalance { get; set; }
    public string? InvoiceAttachmentUrl { get; set; }
    public string? Notes { get; set; }
    public bool IsReviewed { get; set; }
    public bool IsRecevied { get; set; }

    public List<SupplierInvoiceItemResponseDto> InvoiceItems { get; set; } = [];
}

public class SupplierInvoiceItemResponseDto
{
    public Guid Id { get; set; }
    public Guid MedicineUnitId { get; set; }
    public string MedicineName { get; set; } = "";
    public string MedicineUnit { get; set; } = "";
    public decimal Quantity { get; set; }
    public decimal PublicSellingPrice { get; set; }
    public decimal SupplierDiscountPercentage { get; set; }
    public decimal PharmacistFixedMargin { get; set; } 
    public decimal DistributorFixedMargin { get; set; } 
    public decimal TaxAmount { get; set; }
    public DateOnly ExpiryDate { get; set; }
}
