using Pharmacy.Application.Features.Stock.RecorderSettings.Commands.Create;
using Pharmacy.Application.Features.Stock.RecorderSettings.Commands.Update;
using Pharmacy.Application.Features.Stock.RecorderSettings.Queries.GetById;

namespace Pharmacy.Application.Mapping.Stock;

public class RecorderSettingsProfile : MappingProfileBase
{
    public RecorderSettingsProfile()
    {
        CreateMap<CreateRecorderPointCommand, Domain.Entities.Stock.RecorderPointSettings>();
        CreateMap<UpdateRecorderPointCommand, Domain.Entities.Stock.RecorderPointSettings>()
            .IncludeBase<CreateRecorderPointCommand, Domain.Entities.Stock.RecorderPointSettings>();

        CreateMap<Domain.Entities.Stock.RecorderPointSettings, GetRecorderPointResponse>()
            .ForMember(d => d.MedicineName, opt => opt.MapFrom(s => s.Medicine.Name))
            .ForMember(d => d.PreferredSupplierName, opt => opt.MapFrom(s => s.PreferredSupplier!.Name));
    }
}
