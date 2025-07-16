namespace Pharmacy.Domain.Entities.Medicine;

public class MedicineEffectiveMaterialInteraction : BaseEntity
{
    public Guid MedicineId { get; set; }
    public virtual Medicine? Medicine { get; set; }   
    public Guid EffectiveMaterialId { get; set; }
    public virtual EffectiveMaterial.EffectiveMaterial? EffectiveMaterial { get; set; } 
}
