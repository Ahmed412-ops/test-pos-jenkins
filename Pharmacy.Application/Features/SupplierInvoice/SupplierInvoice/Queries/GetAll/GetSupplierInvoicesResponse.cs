
namespace Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Queries.GetAll;

public class GetSupplierInvoicesResponse
{
    public Guid Id { get; set; }
    public required string InvoiceNumber { get; set; }
    public required string SupplierName { get; set; }
    public DateTime InvoiceDate { get; set; }
    public DateTime? DueDate { get; set; }
    public required string PaymentStatus { get; set; } 
    public decimal FinalInvoiceTotal { get; set; }
    public decimal AmountPaid { get; set; }
    public decimal RemainingBalance { get; set; }
    public bool IsReviewed { get; set; }
    public bool IsReceived { get; set; }
}
