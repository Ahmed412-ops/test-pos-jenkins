namespace Pharmacy.Domain.Entities.EffectiveMaterial;

public class EffectiveMaterialFood : BaseEntity
{
    public Guid EffectiveMaterialId { get; set; }
    public virtual EffectiveMaterial? EffectiveMaterial { get; set; }
    public Guid FoodId { get; set; }
    public virtual Food.Food? Food { get; set; }    
}
