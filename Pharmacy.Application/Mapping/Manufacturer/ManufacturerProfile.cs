using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Application.Features.Manufacturer.Commands.Create;
using Pharmacy.Application.Features.Manufacturer.Commands.Update;
using Pharmacy.Application.Features.Manufacturer.Queries.GetAll;
using Pharmacy.Application.Features.Manufacturer.Queries.GetById;

namespace Pharmacy.Application.Mapping.Manufacturer;

public class ManufacturerProfile : MappingProfileBase
{
    public ManufacturerProfile()
    {
        CreateMap<CreateManufacturerCommand, Domain.Entities.Manufacturers.Manufacturer>();
        CreateMap<UpdateManufacturerCommand , Domain.Entities.Manufacturers.Manufacturer>();
        CreateMap<Domain.Entities.Manufacturers.Manufacturer, GetManufacturersResponse>();
        CreateMap<Domain.Entities.Manufacturers.Manufacturer, DropDownQueryResponse>();
        CreateMap<Domain.Entities.Manufacturers.Manufacturer, GetManufacturerResponse>();
    }
}
