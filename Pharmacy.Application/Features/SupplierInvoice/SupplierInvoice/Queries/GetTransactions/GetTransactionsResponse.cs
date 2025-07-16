using Pharmacy.Domain.Enum;

namespace Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Queries.GetTransactions;

public class GetTransactionsResponse 
{
    public Guid Id { get; set; }
    public required string InvoiceNumber { get; set; }
    public Guid SupplierId { get; set; }
    public required string SupplierName { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public required string PaymentMethod { get; set; }
    public string? Notes { get; set; }
    public decimal RemainingBalance { get; set; }
}
