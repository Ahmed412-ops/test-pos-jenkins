namespace Pharmacy.Application.Features.Stock.Medication.Queries.GetById;

public class GetMedicationResponse
{
    public Guid Id { get; set; }
    public Guid MedicineId { get; set; }
    public string MedicineName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public DateOnly ExpiryDate { get; set; }
    public decimal? PurchasePrice { get; set; }
    public decimal SellingPrice { get; set; }
    public string GeneratedBarcode { get; set; } = string.Empty;
}
