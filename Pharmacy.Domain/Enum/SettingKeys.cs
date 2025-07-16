namespace Pharmacy.Domain.Enum;

public enum SettingKeys
{
    //Cashback
    Enabled,
    MaxLimit,
    MinLimit,
    CashbackPercentage,
    MinApplicableAmount,

    // CreditPolicy
    DefaultMaxDebt,
    DefaultGracePeriodDays,
    DebtWarningLimit,
    //Ads
    AdsEnabled,
    InvoiceFooterAds,
}