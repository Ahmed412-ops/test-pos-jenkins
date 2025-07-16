using Pharmacy.Domain.Entities.Customers;
using Pharmacy.Domain.Entities.EffectiveMaterial;

namespace Pharmacy.Domain.Entities.Disease;

public class Disease : EntityModel
{
    public Guid DiseaseCategoryId { get; set; } 
    public virtual required DiseaseCategory DiseaseCategory { get; set; }
    public ICollection<DiseaseSymptom> Symptoms { get; set; } = [];
    public ICollection<EffectiveMaterialDisease> EffectiveMaterialDiseases { get; set; } = [];
    public ICollection<CustomerChronicDisease> CustomerChronicDiseases { get; set; } = [];
}
