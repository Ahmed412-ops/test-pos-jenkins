using Pharmacy.Application.Features.Stock.Return.Commands.Create;
using Pharmacy.Domain.Enum;

namespace Pharmacy.Application.Features.Stock.Return.Queries.GetById;

public class GetMedicationReturnResponse
{
    public required string ReturnReferenceNumber { get; set; } 
    public Guid SupplierId { get; set; } 
    public required string SupplierName { get; set; }
    public DateTime ReturnDate { get; set; }
    public ReturnStatus? ReturnStatus { get; set; } 
    public Guid? SupplierInvoiceId { get; set; }
    public string? SupplierInvoiceNumber { get; set; }
    public ICollection<MedicationReturnItems> ReturnItems { get; set; } = [];
}

public class MedicationReturnItems : MedicationReturnItemDto
{
    public Guid Id { get; set; }
    public string MedicineName { get; set; } = "";
    public string MedicineUnit { get; set; } = "";
}
