using Pharmacy.Application.Dto.Common.Queries;

namespace Pharmacy.Application.Features.EffectiveMaterial.EffectiveMaterial.Queries.GetById;

public class GetEffectiveMaterialResponse : CommonQueryResponseBase
{
    public bool IsChronic { get; set; }
    public string? PatientInformation_En { get; set; }
    public string? PatientInformation_Ar { get; set; }
    public string? BlackBoxWarning { get; set; }    
    public Guid CategoryId { get; set; }
    public required string CategoryName { get; set; } 
    public List<CommonQueryResponseBase> CommonUses { get; set; } = [];
    public List<CommonQueryResponseBase> OffLabelUses { get; set; } = [];
    public List<GetSideEffectsDto> SideEffects { get; set; } = [];
    public List<CommonQueryResponseBase> FoodInteractions { get; set; } = [];
    public List<CommonQueryResponseBase> DiseaseInteraction { get; set; } = [];
    public List<CommonQueryResponseBase> CrossSelling { get; set; } = [];
    public List<CommonQueryResponseBase> DrugInteraction { get; set; } = [];
    public List<CommonQueryResponseBase> MedicinesDrugInteractions { get; set; } = [];
    public List<CommonQueryResponseBase> MedicinesCrossSelling { get; set; } = [];
}

public class GetSideEffectsDto : CommonQueryResponseBase
{
    public float Probability { get; set; }
    public bool IsMajor { get; set; }
}