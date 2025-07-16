using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Application.Features.Uses.Commands.Create;
using Pharmacy.Application.Features.Uses.Commands.Update;
using Pharmacy.Application.Features.Uses.Queries.GetAll;
using Pharmacy.Application.Features.Uses.Queries.GetById;

namespace Pharmacy.Application.Mapping.Uses;

public class UsesProfile : MappingProfileBase
{
    public UsesProfile()
    {
        CreateMap<CreateUseCommand, Domain.Entities.Uses.Use>();
        CreateMap<UpdateUseCommand, Domain.Entities.Uses.Use>();
        CreateMap<Domain.Entities.Uses.Use, GetUsesResponse>();
        CreateMap<Domain.Entities.Uses.Use, DropDownQueryResponse>();
        CreateMap<Domain.Entities.Uses.Use, GetUseResponse>();
    }
}
