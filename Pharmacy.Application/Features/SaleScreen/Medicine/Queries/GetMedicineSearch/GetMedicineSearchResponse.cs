namespace Pharmacy.Application.Features.SaleScreen.Medicine.Queries.GetMedicineSearch;

public class GetMedicineSearchResponse
{
    public Guid MedicineId { get; set; }
    public string MedicineName { get; set; } = string.Empty;
    public decimal TotalQuantity { get; set; }
    public decimal HighestPrice { get; set; }
}
