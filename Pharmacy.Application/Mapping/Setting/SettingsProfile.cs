using Pharmacy.Application.Features.Settings.Commands.Update;
using Pharmacy.Application.Features.Settings.Queries.SystemSettingResponses;
using Pharmacy.Domain.Entities.Settings;

namespace Pharmacy.Application.Mapping;

public class SettingsProfile : MappingProfileBase
{
    public SettingsProfile()
    {
        CreateMap<UpdateSystemSettingCommand, SystemSetting>();

        CreateMap<SystemSetting, SystemSettingResponse>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
            ;
    }
}
