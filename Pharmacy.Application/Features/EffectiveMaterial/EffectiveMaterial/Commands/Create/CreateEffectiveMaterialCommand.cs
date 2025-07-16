using MediatR;
using Pharmacy.Application.Dto.Common.Commands;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.EffectiveMaterial.EffectiveMaterial.Commands.Create;

public class CreateEffectiveMaterialCommand : IBaseCommand, IRequest<Result<string>>
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public bool IsChronic { get; set; } = false;
    public string? PatientInformation_En { get; set; }
    public string? PatientInformation_Ar { get; set; }
    public string? BlackBoxWarning { get; set; }
    public Guid CategoryId { get; set; } 
    public List<SideEffectsDto> SideEffects { get; set; } = [];
    public List<Guid> CommonUses { get; set; } = [];
    public List<Guid> OffLabelUses { get; set; } = [];
    public List<Guid> FoodInteractions { get; set; } = [];
    public List<Guid> DiseaseInteraction { get; set; } = [];
    public List<Guid> CrossSelling { get; set; } = [];
    public List<Guid> DrugInteraction { get; set; } = [];
    public List<Guid> MedicinesDrugInteractions { get; set; } = [];
    public List<Guid> MedicinesCrossSelling { get; set; } = [];
}

public class SideEffectsDto
{
    public Guid SideEffectId { get; set; }
    public float Probability { get; set; }
    public bool IsMajor { get; set; }
}