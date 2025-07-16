using Pharmacy.Domain.Entities.SideEffects;

namespace Pharmacy.Domain.Entities.EffectiveMaterial;

public class EffectiveMaterialSideEffect : BaseEntity
{
    public Guid EffectiveMaterialId { get; set; }
    public virtual EffectiveMaterial? EffectiveMaterial { get; set; } 
    public Guid SideEffectId { get; set; }
    public virtual SideEffect? SideEffect { get; set; }    
    public float Probability  { get; set; }
    public bool IsMajor { get; set; } 
}
