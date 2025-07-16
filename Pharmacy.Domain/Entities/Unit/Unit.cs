namespace Pharmacy.Domain.Entities.Unit;

public class Unit : EntityModel
{
    public ICollection<Medicine.MedicineUnit> Medicines { get; set; } = [];
}
