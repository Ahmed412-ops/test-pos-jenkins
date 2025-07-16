using Pharmacy.Domain.Entities.EffectiveMaterial;

namespace Pharmacy.Domain.Entities.Food;

public class Food : EntityModel
{
    public  ICollection<EffectiveMaterialFood> EffectiveMaterialFoods { get; set; } = [];
}
