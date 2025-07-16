namespace Pharmacy.Application.Dto.MedicineConflict;

public class ConflictDto
{
    public Guid OtherMedicineId { get; set; }
    public required string OtherMedicineName { get; set; }
    public ConflictType Type { get; set; }
}

public enum ConflictType
{
    DrugDrug,
    DrugFood,
    DrugDisease,
    CrossSell,
}
