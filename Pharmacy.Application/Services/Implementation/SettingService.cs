using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Helper.Extensions;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Application.Services.Abstraction.SettingService;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Entities.Settings;
using Pharmacy.Domain.Enum;

namespace Pharmacy.Application.Services.Implementation
{
    public class SettingService(IUnitOfWork unitOfWork) : ISettingService
    {
        private readonly IGenericRepository<SystemSetting> _settingRepo
            = unitOfWork.GetRepository<SystemSetting>();
        public async Task<Result<T>> GetValueAsync<T>(SettingsModules module, SettingKeys key, T? defaultValue = default)
        {
            var setting = await _settingRepo.FindAsync(s => s.Module == module && s.Key == key);

            if (setting == null)
                return Result<T>.Fail(Messages.SettingNotFound);

            if (string.IsNullOrWhiteSpace(setting.Value))
                return Result<T>.Success(defaultValue!);

            var conversionResult = SettingExtensions.ConvertValue<T>(setting.Value, setting.Type.ToString());

            return conversionResult.Succeeded
                ? conversionResult
                : Result<T>.Fail(conversionResult.Messages);
        }

        public async Task<Result<Dictionary<SettingKeys, object>>> GetMultipleValuesAsync(
            SettingsModules module, Dictionary<SettingKeys, Type> keysWithTypes)
        {
            var keys = keysWithTypes.Keys.ToList();
            var settings = await _settingRepo.GetAllAsync(
                s => s.Module == module && keys.Contains(s.Key)
            );
            var result = new Dictionary<SettingKeys, object>();
            var messages = new List<string>();

            foreach (var key in keys)
            {
                var setting = settings.FirstOrDefault(s => s.Key == key);
                if (setting == null || string.IsNullOrWhiteSpace(setting.Value))
                {
                    messages.Add(Messages.SettingNotFound);
                    continue;
                }

                var targetType = keysWithTypes[key];
                var converted = SettingExtensions.ConvertValue(setting.Value, setting.Type.ToString(), targetType);

                if (!converted.Succeeded)
                {
                    messages.AddRange(converted.Messages.Select(m => $"{key}: {m}"));
                }
                else
                {
                    result[key] = converted.Data!;
                }
            }

            if (messages.Any())
                return Result<Dictionary<SettingKeys, object>>.Fail(messages);

            return Result<Dictionary<SettingKeys, object>>.Success(result);
        }

    }

}
