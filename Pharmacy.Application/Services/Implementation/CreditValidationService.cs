using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Application.Services.Abstraction.SettingService;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Entities.Customers;
using Pharmacy.Domain.Entities.Wallets.Sales;
using Pharmacy.Domain.Enum;

namespace Pharmacy.Application.Services.Implementation
{
    public class CreditValidationService(IUnitOfWork unitOfWork, ISettingService settingService) : ICreditValidationService
    {
        private readonly IGenericRepository<Prescription> _prescription =
        unitOfWork.GetRepository<Prescription>();
        private readonly IGenericRepository<Customer> _customer =
        unitOfWork.GetRepository<Customer>();
        public async Task<Result<Prescription>> ValidateAsync(Prescription newPrescription)
        {
            var customer = await _customer.FindAsync(c => c.Id == newPrescription.CustomerId);
            if (customer == null)
                return Result<Prescription>.Fail(Messages.CustomerNotFound);
            if (newPrescription.AmountDue <= 0)
                return Result<Prescription>.Success(newPrescription);

            var settingsResult = await LoadValidationSettingsAsync();
            if (!settingsResult.Succeeded)
                return Result<Prescription>.Fail(settingsResult.Messages);
            var (maxDebt, graceDays, warningPercent) = settingsResult.Data;

            // Check for exceeding max debt
            var (exceedsMaxDebt, totalDebt) = await ExceedsMaxDebtAsync(customer.Id, newPrescription, maxDebt);
            if (exceedsMaxDebt)
                return Result<Prescription>.Fail(Messages.DebtExceedsLimit);

            if (await HasOverdueDebtsAsync(customer.Id, graceDays))
                return Result<Prescription>.Fail(Messages.OverdueInvoicesExceededGracePeriod);


            return Result<Prescription>.Success(newPrescription);
        }

        private async Task<Result<(decimal MaxDebt, int GraceDays, decimal WarningPercent)>> LoadValidationSettingsAsync()
        {
            var maxDebtResult = await settingService.
            GetValueAsync(SettingsModules.CreditPolicy, SettingKeys.DefaultMaxDebt, 1000m);
            var gracePeriodResult = await settingService.
            GetValueAsync(SettingsModules.CreditPolicy, SettingKeys.DefaultGracePeriodDays, 30);
            var warningPercentResult = await settingService.
            GetValueAsync(SettingsModules.CreditPolicy, SettingKeys.DebtWarningLimit, 80m);

            var messages = new List<string>();
            messages.AddRange(maxDebtResult.Messages ?? []);
            messages.AddRange(gracePeriodResult.Messages ?? []);
            messages.AddRange(warningPercentResult.Messages ?? []);

            if (messages.Any())
                return Result<(decimal MaxDebt, int GraceDays, decimal WarningPercent)>.Fail(messages);

            return Result<(decimal MaxDebt, int GraceDays, decimal WarningPercent)>.Success(
                (maxDebtResult.Data, gracePeriodResult.Data, warningPercentResult.Data)
            );
        }

        private async Task<(bool exceedsMaxDebt, decimal totalDebt)> ExceedsMaxDebtAsync
        (Guid customerId, Prescription newPrescription, decimal maxDebt)
        {
            var prescriptions = await _prescription.GetAllAsync(
                p => p.CustomerId == customerId && p.Id != newPrescription.Id,
                Include: a => a.Include(p => p.Items).Include(t => t.Transactions)
            );
            var currentDebt = prescriptions
            .Select(p => p.AmountDue).
            Where(amountDue => amountDue > 0).Sum();

            var totalDebt = currentDebt + newPrescription.AmountDue;
            var exceedsMaxDebt = totalDebt > maxDebt;
            return (exceedsMaxDebt, totalDebt);
        }
        private async Task<bool> HasOverdueDebtsAsync(Guid customerId, int graceDays)
        {
            var now = DateTime.UtcNow;
            var cutoffDate = now.AddDays(-graceDays);

            var prescriptions = await _prescription.GetAllAsync(
                p => p.CustomerId == customerId && p.Created_At < cutoffDate,
                Include: q => q.Include(p => p.Items).Include(p => p.Transactions)
            );

            return prescriptions.Any(p => p.AmountDue > 0);
        }
        // private bool IsApproachingDebtLimit(decimal totalDebt, decimal maxDebt, decimal warningPercent)
        // {
        //     var warningLimit = maxDebt * warningPercent / 100;
        //     return totalDebt >= warningLimit;
        // }
        // private Result<Prescription> Fail(Prescription prescription, string reason)
        // {
        //     prescription.ValidationFailed = true;
        //     prescription.FailedReason = reason;
        //     return Result.Failure<Prescription>(reason);
        // }
    }
}