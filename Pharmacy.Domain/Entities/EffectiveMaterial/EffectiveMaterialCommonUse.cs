using Pharmacy.Domain.Entities.Uses;

namespace Pharmacy.Domain.Entities.EffectiveMaterial;

public class EffectiveMaterialCommonUse : BaseEntity
{
    public Guid EffectiveMaterialId { get; set; }
    public virtual EffectiveMaterial? EffectiveMaterial { get; set; } 
    public Guid UseId { get; set; }
    public virtual Use? Use { get; set; } 
}
