namespace Pharmacy.Domain.Entities.EffectiveMaterial;

public class EffectiveMaterialCrossSelling : BaseEntity
{
    public Guid EffectiveMaterialId { get; set; }
    public EffectiveMaterial EffectiveMaterial { get; set; } = null!; 

    public Guid CrossSellingMaterialId { get; set; }
    public EffectiveMaterial CrossSellingMaterial { get; set; } = null!;
}
