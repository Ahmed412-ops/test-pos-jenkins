namespace Pharmacy.Application.Features.Stock.Medication.Queries.GetLevel;

public class GetLevelsResponse
{
    public Guid MedicineId { get; set; }
    public string MedicineName { get; set; } = string.Empty;
    public decimal TotalQuantity { get; set; }
    public Guid? RecorderPointId { get; set; }
    public decimal ReorderPoint { get; set; } = 0;
    public decimal RestockingQuantity { get; set; } = 0;
    public bool IsBelowReorderPoint { get; set; }
}
