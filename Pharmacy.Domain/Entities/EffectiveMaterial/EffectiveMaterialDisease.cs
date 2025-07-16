namespace Pharmacy.Domain.Entities.EffectiveMaterial;

public class EffectiveMaterialDisease : BaseEntity
{
    public Guid EffectiveMaterialId { get; set; }
    public virtual EffectiveMaterial? EffectiveMaterial { get; set; }
    public Guid DiseaseId { get; set; }
    public virtual Disease.Disease? Disease { get; set; }
}
