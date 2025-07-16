using Pharmacy.Domain.Entities.EffectiveMaterial;

namespace Pharmacy.Domain.Entities.Uses;

public class Use : EntityModel
{
    public  ICollection<EffectiveMaterialCommonUse> EffectiveMaterialCommonUses { get; set; } = [];
    public  ICollection<EffectiveMaterialOffLabelUse> EffectiveMaterialOffLabelUses { get; set; } = [];
}
