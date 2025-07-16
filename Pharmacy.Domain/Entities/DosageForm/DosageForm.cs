namespace Pharmacy.Domain.Entities.DosageForm;

public class DosageForm : EntityModel
{
    public bool Is_Liquid { get; set; } 
    public bool AffectsDrugInteractions { get; set; } 
    public ICollection<Medicine.Medicine> Medicines { get; set; } = [];
}
