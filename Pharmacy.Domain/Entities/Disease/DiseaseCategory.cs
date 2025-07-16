namespace Pharmacy.Domain.Entities.Disease;

public class DiseaseCategory : EntityModel
{
    public ICollection<Disease> Diseases { get; set; } = [];
}
