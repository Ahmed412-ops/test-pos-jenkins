using Pharmacy.Domain.Entities.Disease;

namespace Pharmacy.Domain.Entities.Symptoms;

public class Symptom : EntityModel
{
    public ICollection<DiseaseSymptom> Diseases { get; set; } = [];
}
