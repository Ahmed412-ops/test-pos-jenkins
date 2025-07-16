using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Enum;

namespace Pharmacy.Application.Services.Abstraction.SettingService;

public interface ISettingService
{
    public  Task<Result<T>> GetValueAsync<T>
        (SettingsModules module, SettingKeys key, T? defaultValue = default);
    public Task<Result<Dictionary<SettingKeys, object>>> GetMultipleValuesAsync(
            SettingsModules module, Dictionary<SettingKeys, Type> keysWithTypes);
}
