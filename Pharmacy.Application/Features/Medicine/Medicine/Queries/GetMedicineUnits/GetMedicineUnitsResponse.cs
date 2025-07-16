namespace Pharmacy.Application.Features.Medicine.Medicine.Queries.GetMedicineUnits;

public class GetMedicineUnitsResponse
{
    public string MedicineName { get; set; } = string.Empty;
    public List<UnitResponse> Units { get; set; } = [];
}
public class UnitResponse
{
    public Guid MedicineUnitId { get; set; }
    public string UnitName { get; set; } = string.Empty;
}