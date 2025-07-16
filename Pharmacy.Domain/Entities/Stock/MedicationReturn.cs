using Pharmacy.Domain.Enum;

namespace Pharmacy.Domain.Entities.Stock;

public class MedicationReturn : BaseEntity
{
    public required string ReturnReferenceNumber { get; set; } 
    public Guid SupplierId { get; set; } 
    public Supplier.Supplier Supplier { get; set; } = null!;
    public DateTime ReturnDate { get; set; }
    public ReturnStatus ReturnStatus { get; set; }
    public Guid? SupplierInvoiceId { get; set; }
    public SupplierInvoice.SupplierInvoice? SupplierInvoice { get; set; } 
    public ICollection<MedicationReturnItem> ReturnItems { get; set; } = [];
    
}
