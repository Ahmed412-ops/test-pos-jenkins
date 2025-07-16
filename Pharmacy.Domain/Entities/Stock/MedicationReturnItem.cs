using Pharmacy.Domain.Entities.Medicine;
using Pharmacy.Domain.Enum;

namespace Pharmacy.Domain.Entities.Stock;

public class MedicationReturnItem : BaseEntity
{
    public Guid MedicationReturnId { get; set; }
    public MedicationReturn MedicationReturn { get; set; } = null!;
    public Guid MedicineUnitId { get; set; }
    public MedicineUnit MedicineUnit { get; set; } = null!;  
    public decimal QuantityToReturn { get; set; }
    public required DateOnly ExpiryDate { get; set; }
    public string Barcode { get; set; } = string.Empty;
    public ReturnReason ReturnReason { get; set; }
    public string? AdditionalReasonDetails { get; set; }
    public decimal ReturnValue { get; set; }
}
