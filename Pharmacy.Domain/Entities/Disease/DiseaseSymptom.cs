using Pharmacy.Domain.Entities.Symptoms;

namespace Pharmacy.Domain.Entities.Disease;

public class DiseaseSymptom : BaseEntity
{
    public Guid DiseaseId { get; set; }
    public virtual Disease? Disease { get; set; }
    public Guid SymptomId { get; set; }
    public virtual Symptom? Symptom { get; set; }    
}
