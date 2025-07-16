using Pharmacy.Domain.Entities.EffectiveMaterial;

namespace Pharmacy.Domain.Entities.SideEffects;

public class SideEffect : EntityModel
{
    public ICollection<EffectiveMaterialSideEffect> EffectiveMaterialSideEffects { get; set; } = [];
}
