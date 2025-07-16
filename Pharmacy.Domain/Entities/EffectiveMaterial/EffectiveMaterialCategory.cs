namespace Pharmacy.Domain.Entities.EffectiveMaterial;

public class EffectiveMaterialCategory : EntityModel
{
    public ICollection<EffectiveMaterial> EffectiveMaterials { get; set; } = [];
}
