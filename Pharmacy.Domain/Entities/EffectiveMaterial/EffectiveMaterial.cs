
using Pharmacy.Domain.Entities.Medicine;

namespace Pharmacy.Domain.Entities.EffectiveMaterial;

public class EffectiveMaterial : EntityModel
{
    public bool IsChronic { get; set; } = false;
    public string? PatientInformation_En { get; set; }
    public string? PatientInformation_Ar { get; set; }
    public string? BlackBoxWarning { get; set; }
    public Guid CategoryId { get; set; } 
    public required EffectiveMaterialCategory Category { get; set; }

    // Navigation properties many-to-many relationships
    public ICollection<EffectiveMaterialCommonUse> CommonUses { get; set; } = [];
    public ICollection<EffectiveMaterialOffLabelUse> OffLabelUses { get; set; } = [];
    public ICollection<EffectiveMaterialSideEffect> SideEffects { get; set; } = [];
    public ICollection<EffectiveMaterialFood> FoodInteractions { get; set; } = [];
    public ICollection<EffectiveMaterialDisease> DiseaseInteraction { get; set; } = [];
    // Medicine Navigation properties one-to-many relationships
    public ICollection<MedicineEffectiveMaterial> Medicines { get; set; } = [];
    public ICollection<MedicineEffectiveMaterialInteraction> MedicinesDrugInteractions { get; set; } = [];
    public ICollection<MedicineEffectiveMaterialCrossSelling> MedicinesCrossSelling { get; set; } = [];
    // Self-join relationships 
    public ICollection<EffectiveMaterialCrossSelling> EM_CrossSelling { get; set; } = [];
    public ICollection<EffectiveMaterialCrossSelling> CS_EffectiveMaterials { get; set; } = [];

    public ICollection<EffectiveMaterialDrugInteraction> EM_DrugInteractions { get; set; } = [];
    public ICollection<EffectiveMaterialDrugInteraction> DI_EffectiveMaterials { get; set; } = [];

}
