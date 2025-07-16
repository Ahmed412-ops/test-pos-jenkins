using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Application.Features.EffectiveMaterial.Category.Commands.Create;
using Pharmacy.Application.Features.EffectiveMaterial.Category.Commands.Update;
using Pharmacy.Application.Features.EffectiveMaterial.Category.Queries.GetAll;
using Pharmacy.Application.Features.EffectiveMaterial.Category.Queries.GetById;

namespace Pharmacy.Application.Mapping.EffectiveMaterial;

public class EffectiveMaterialCategoryProfile : MappingProfileBase
{
    public EffectiveMaterialCategoryProfile()
    {
        CreateMap<CreateEffectiveMaterialCategoryCommand, Domain.Entities.EffectiveMaterial.EffectiveMaterialCategory>();
        CreateMap<Domain.Entities.EffectiveMaterial.EffectiveMaterialCategory, GetEffectiveMaterialCategoriesResponse>();   
        CreateMap<Domain.Entities.EffectiveMaterial.EffectiveMaterialCategory, DropDownQueryResponse>();
        CreateMap<UpdateEffectiveMaterialCategoryCommand, Domain.Entities.EffectiveMaterial.EffectiveMaterialCategory>();
        CreateMap<Domain.Entities.EffectiveMaterial.EffectiveMaterialCategory, GetEffectiveMaterialCategoryResponse>()
            .ForMember(dest => dest.EffectiveMaterials, 
                       opt => opt.MapFrom(src => src.EffectiveMaterials));
    }
}
