namespace Pharmacy.Application.Features.Stock.Medication.Queries.GetAll;

public class GetMedicationsResponse
{
    public Guid Id { get; set; }
    public Guid MedicineId { get; set; }
    public string MedicineName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public DateOnly ExpiryDate { get; set; }
    public decimal SellingPrice { get; set; }
    public string GeneratedBarcode { get; set; } = string.Empty;
}
