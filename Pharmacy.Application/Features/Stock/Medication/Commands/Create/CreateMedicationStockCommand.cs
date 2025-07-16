using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Stock.Medication.Commands.Create;

public class CreateMedicationStockCommand : IRequest<Result<string>>
{
    public Guid MedicineId { get; set; }
    public decimal Quantity { get; set; }
    public DateOnly ExpiryDate { get; set; }
    public decimal? PurchasePrice { get; set; }
    public decimal SellingPrice { get; set; }
}
