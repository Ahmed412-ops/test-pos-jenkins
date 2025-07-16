using Pharmacy.Application.Resources.Static;
using Pharmacy.Application.Services.Abstraction.CashBackServices;
using Pharmacy.Application.Services.Abstraction.SettingService;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Entities.Wallets.Sales;
using Pharmacy.Domain.Enum;

namespace Pharmacy.Application.Services.Implementation
{
    public class CashbackService(ISettingService _settingService) : ICashbackService
    {
        public async Task<Result<bool>> ValidateCashbackEligibility(Prescription prescription)
        {
            var isEnabledResult = await _settingService.GetValueAsync(
                SettingsModules.Cashback,
                SettingKeys.Enabled,
                defaultValue: false
            );

            if (!isEnabledResult.Succeeded || !isEnabledResult.Data)
                return Result<bool>.Fail(Messages.CashbackSystemDisabled);

            var minAmountResult = await _settingService.GetValueAsync<decimal>(
                SettingsModules.Cashback,
                SettingKeys.MinApplicableAmount,
                defaultValue: 500
            );

            if (!minAmountResult.Succeeded || prescription.Amount < minAmountResult.Data)
                return Result<bool>.Fail(Messages.CashbackNotApplicableForLowAmount);

            return Result<bool>.Success(true);
        }

        public async Task<decimal> CalculateEarnedCashback(decimal prescriptionAmount)
        {
            var settings = await _settingService.GetMultipleValuesAsync(
                SettingsModules.Cashback,
                new Dictionary<SettingKeys, Type>
                {
                    [SettingKeys.CashbackPercentage] = typeof(decimal),
                    [SettingKeys.MaxLimit] = typeof(decimal),
                    [SettingKeys.MinLimit] = typeof(decimal),
                }
            );

            var percentage = (decimal)settings.Data[SettingKeys.CashbackPercentage];
            var maxLimit = (decimal)settings.Data[SettingKeys.MaxLimit];
            var minLimit = (decimal)settings.Data[SettingKeys.MinLimit];

            var earned = prescriptionAmount * percentage / 100;
            return Math.Clamp(earned, minLimit, maxLimit);
        }
    }
}
