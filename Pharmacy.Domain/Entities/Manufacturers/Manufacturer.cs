namespace Pharmacy.Domain.Entities.Manufacturers;

public class Manufacturer : EntityModel
{
    public ICollection<Medicine.Medicine> Medicines { get; set; } = [];
}
