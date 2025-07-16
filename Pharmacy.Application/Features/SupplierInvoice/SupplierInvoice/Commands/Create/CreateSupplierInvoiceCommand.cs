using MediatR;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Enum;

namespace Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Commands.Create;

public class CreateSupplierInvoiceCommand : IRequest<Result<string>>
{
    public required string InvoiceNumber { get; set; }
    public required Guid SupplierId { get; set; }
    public Guid? PurchaseOrderId { get; set; }
    public DateTime InvoiceDate { get; set; }
    public DateTime? DueDate { get; set; }
    public decimal ShippingFees { get; set; } = 0;
    // Payment Information
    // public PaymentStatus PaymentStatus { get; set; }
    public Guid? ShiftWalletId { get; set; }
    public decimal AmountPaid { get; set; }
    public DateTime? PaymentDate { get; set; } // Required if PaymentStatus is Paid/PartiallyPaid
    public PaymentMethod? PaymentMethod { get; set; } // Required if PaymentStatus is Paid/PartiallyPaid
    public bool IsRecevied { get; set; } = false;
    public string? InvoiceAttachmentUrl { get; set; }
    public string? Notes { get; set; }
    public List<CreateSupplierInvoiceItemDto> InvoiceItems { get; set; } = [];
}

public class CreateSupplierInvoiceItemDto
{
    public required Guid MedicineUnitId { get; set; }
    public decimal Quantity { get; set; }
    public required decimal PublicSellingPrice { get; set; } // Retail price
    public decimal SupplierDiscountPercentage { get; set; } = 0; // E.g., 0.10 for 10% discount
    public decimal PharmacistFixedMargin { get; set; } = 0;
    public decimal DistributorFixedMargin { get; set; } = 0;
    public decimal TaxAmount { get; set; } // Tax rate for the item
    public required DateOnly ExpiryDate { get; set; }
}
