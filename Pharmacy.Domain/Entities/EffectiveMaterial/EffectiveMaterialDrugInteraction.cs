namespace Pharmacy.Domain.Entities.EffectiveMaterial;

public class EffectiveMaterialDrugInteraction : BaseEntity
{
    public Guid EffectiveMaterialId { get; set; }
    public EffectiveMaterial EffectiveMaterial { get; set; } = null!;

    public Guid InteractingMaterialId { get; set; }
    public EffectiveMaterial InteractingMaterial { get; set; } = null!;  
}
