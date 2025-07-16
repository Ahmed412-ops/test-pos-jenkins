using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Application.Features.Unit.Commands.Create;
using Pharmacy.Application.Features.Unit.Commands.Update;
using Pharmacy.Application.Features.Unit.Queries.GetAll;
using Pharmacy.Application.Features.Unit.Queries.GetById;

namespace Pharmacy.Application.Mapping.Unit;

public class UnitProfile : MappingProfileBase
{
    public UnitProfile()
    {
        CreateMap<CreateUnitCommand, Domain.Entities.Unit.Unit>();
        CreateMap<UpdateUnitCommand, Domain.Entities.Unit.Unit>();
        CreateMap<Domain.Entities.Unit.Unit, GetUnitsResponse>();
        CreateMap<Domain.Entities.Unit.Unit, DropDownQueryResponse>();
        CreateMap<Domain.Entities.Unit.Unit, GetUnitResponse>();        
    }
}
