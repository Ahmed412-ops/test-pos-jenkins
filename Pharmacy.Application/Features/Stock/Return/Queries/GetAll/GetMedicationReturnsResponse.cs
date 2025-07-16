namespace Pharmacy.Application.Features.Stock.Return.Queries.GetAll;

public class GetMedicationReturnsResponse
{
    public Guid Id { get; set; }
    public required string ReturnReferenceNumber { get; set; } 
    public required string SupplierName { get; set; }
    public DateTime ReturnDate { get; set; }
    public required string ReturnStatus { get; set; }
    public Guid? SupplierInvoiceId { get; set; }
    public string? SupplierInvoiceNumber { get; set; }

}
