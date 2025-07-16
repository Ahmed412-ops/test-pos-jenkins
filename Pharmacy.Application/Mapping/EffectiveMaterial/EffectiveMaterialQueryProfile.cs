using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Application.Features.EffectiveMaterial.EffectiveMaterial.Queries.GetAll;
using Pharmacy.Application.Features.EffectiveMaterial.EffectiveMaterial.Queries.GetById;

namespace Pharmacy.Application.Mapping.EffectiveMaterial;

public class EffectiveMaterialQueryProfile : MappingProfileBase
{
    public EffectiveMaterialQueryProfile()
    {
        CreateMap<Domain.Entities.EffectiveMaterial.EffectiveMaterial, GetEffectiveMaterialsResponse>();
        
        CreateMap<Domain.Entities.EffectiveMaterial.EffectiveMaterial, DropDownQueryResponse>();
        CreateMap<Domain.Entities.EffectiveMaterial.EffectiveMaterial, GetEffectiveMaterialResponse>()
            .ForMember(dest => dest.CategoryId, 
                opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.SideEffects, 
                opt => opt.MapFrom(src => src.SideEffects.Select(s => s)))
            .ForMember(dest => dest.CategoryName,
                opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.CommonUses, 
                opt => opt.MapFrom(src => src.CommonUses.Select(c => c.Use)))
            .ForMember(dest => dest.OffLabelUses, 
                opt => opt.MapFrom(src => src.OffLabelUses.Select(o => o.Use)))
            .ForMember(dest => dest.FoodInteractions, 
                opt => opt.MapFrom(src => src.FoodInteractions.Select(f => f.Food)))
            .ForMember(dest => dest.DiseaseInteraction, 
                opt => opt.MapFrom(src => src.DiseaseInteraction.Select(d => d.Disease)))
            .ForMember(dest => dest.CrossSelling, 
                opt => opt.MapFrom(src => src.EM_CrossSelling.Select(c => c.CrossSellingMaterial)))
            .ForMember(dest => dest.DrugInteraction, 
                opt => opt.MapFrom(src => src.EM_DrugInteractions.Select(d => d.InteractingMaterial)))
            .ForMember(dest => dest.MedicinesDrugInteractions, 
                opt => opt.MapFrom(src => src.MedicinesDrugInteractions.Select(m => m.Medicine)))
            .ForMember(dest => dest.MedicinesCrossSelling, 
                opt => opt.MapFrom(src => src.MedicinesCrossSelling.Select(m => m.Medicine)));

        CreateMap<Domain.Entities.SideEffects.SideEffect, CommonQueryResponseBase>();
        CreateMap<Domain.Entities.Uses.Use, CommonQueryResponseBase>();
        CreateMap<Domain.Entities.Food.Food, CommonQueryResponseBase>();
        CreateMap<Domain.Entities.Disease.Disease, CommonQueryResponseBase>();
        CreateMap<Domain.Entities.EffectiveMaterial.EffectiveMaterial, CommonQueryResponseBase>();
        CreateMap<Domain.Entities.EffectiveMaterial.EffectiveMaterialSideEffect, GetSideEffectsDto>()
            .ForMember(dest => dest.Id, 
                opt => opt.MapFrom(src => src.SideEffectId)) 
            .ForMember(dest => dest.Name, 
                opt => opt.MapFrom(src => src.SideEffect!.Name)) 
            .ForMember(dest => dest.Probability, 
                opt => opt.MapFrom(src => src.Probability))
            .ForMember(dest => dest.IsMajor, 
                opt => opt.MapFrom(src => src.IsMajor));

    }
}
