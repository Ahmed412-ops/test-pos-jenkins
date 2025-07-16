namespace Pharmacy.Application.Features.Stock.RecorderSettings.Queries.GetById;

public class GetRecorderPointResponse
{
    public Guid MedicineId { get; set; }
    public string MedicineName { get; set; } = string.Empty;
    public Guid? PreferredSupplierId { get; set; }
    public string PreferredSupplierName { get; set; } = string.Empty;
    public decimal ReorderPoint { get; set; }
    public decimal RestockingQuantity { get; set; }
    public bool NotificationsEnabled { get; set; } = true;
}
