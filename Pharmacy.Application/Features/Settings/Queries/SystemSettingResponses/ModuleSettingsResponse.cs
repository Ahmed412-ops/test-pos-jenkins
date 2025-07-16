using Pharmacy.Domain.Enum;

namespace Pharmacy.Application.Features.Settings.Queries.SystemSettingResponses;

public class ModuleSettingsResponse
{
    public string Module { get; set; }  = string.Empty;
    public List<SystemSettingResponse> Settings { get; set; } = new(); 
}
