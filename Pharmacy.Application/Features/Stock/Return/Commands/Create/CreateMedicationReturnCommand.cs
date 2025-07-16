using MediatR;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Enum;

namespace Pharmacy.Application.Features.Stock.Return.Commands.Create;

public class CreateMedicationReturnCommand : IRequest<Result<string>> 
{
    public required string ReturnReferenceNumber { get; set; } 
    public Guid SupplierId { get; set; } 
    public DateTime ReturnDate { get; set; }
    public ReturnStatus ReturnStatus { get; set; }
    public Guid? SupplierInvoiceId { get; set; }
    public ICollection<MedicationReturnItemDto> ReturnItems { get; set; } = [];
}

public class MedicationReturnItemDto
{
    public Guid MedicineUnitId { get; set; }
    public decimal QuantityToReturn { get; set; }
    public DateOnly ExpiryDate { get; set; }
    public ReturnReason ReturnReason { get; set; }
    public string? AdditionalReasonDetails { get; set; }
    public decimal ReturnValue { get; set; }
    public required string Barcode { get; set; }
}