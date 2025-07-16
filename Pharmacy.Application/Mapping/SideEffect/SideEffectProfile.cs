using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Application.Features.SideEffect.Commands.Create;
using Pharmacy.Application.Features.SideEffect.Commands.Update;
using Pharmacy.Application.Features.SideEffect.Queries.GetAll;
using Pharmacy.Application.Features.SideEffect.Queries.GetById;

namespace Pharmacy.Application.Mapping.SideEffect;

public class SideEffectProfile : MappingProfileBase
{
    public SideEffectProfile()
    {
        CreateMap<CreateSideEffectCommand, Domain.Entities.SideEffects.SideEffect>();
        CreateMap<UpdateSideEffectCommand, Domain.Entities.SideEffects.SideEffect>();
        CreateMap<Domain.Entities.SideEffects.SideEffect, GetSideEffectsResponse>();
        CreateMap<Domain.Entities.SideEffects.SideEffect, GetSideEffectResponse>()
            .ForMember(dest => dest.EffectiveMaterials, 
                opt => opt.MapFrom(src => src.EffectiveMaterialSideEffects
                                            .Select(em => em.EffectiveMaterial!.Name))); 
        CreateMap<Domain.Entities.SideEffects.SideEffect, DropDownQueryResponse>(); 
    }
}
