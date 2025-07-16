using Pharmacy.Domain.Enum;

namespace Pharmacy.Domain.Entities.Settings;

public class SystemSetting : BaseEntity
{
    public SettingKeys Key { get; set; }
    public required string Value { get; set; }
    public SettingsModules Module { get; set; }
    public SettingType Type { get; set; }
}
