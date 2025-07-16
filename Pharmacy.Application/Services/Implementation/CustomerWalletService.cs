using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Services.Abstraction.CustomerBalanceService;
using Pharmacy.Domain.Entities.Wallets.Sales;

namespace Pharmacy.Application.Services.Implementation;

public class CustomerWalletService(IUnitOfWork unitOfWork) : ICustomerWalletService
{
    private readonly IGenericRepository<BalanceTransaction> _balanceRepo =
        unitOfWork.GetRepository<BalanceTransaction>();

    private async Task LogTransactionAsync(
        CustomerTransactionType type,
        Guid customerId,
        Guid? prescriptionId,
        decimal amount
    )
    {
        if (amount <= 0 || customerId == Guid.Empty)
            return;

        await _balanceRepo.AddAsync(
            new BalanceTransaction
            {
                CustomerId = customerId,
                PrescriptionId = prescriptionId,
                Type = type,
                Amount = amount,
            }
        );
    }

    public Task LogNormalPaymentAsync(Guid customerId, Guid prescriptionId, decimal amount) =>
        LogTransactionAsync(
            CustomerTransactionType.NormalPayment,
            customerId,
            prescriptionId,
            amount
        );

    public Task LogCreditUsageAsync(Guid customerId, Guid prescriptionId, decimal amount) =>
        LogTransactionAsync(CustomerTransactionType.CreditUsed, customerId, prescriptionId, amount);

    public async Task<decimal> RecordOverpaymentAsync(
        Guid customerId,
        Guid prescriptionId,
        decimal paidAmount,
        decimal dueAmount
    )
    {
        var overpayment = paidAmount - dueAmount;
        if (overpayment <= 0 || customerId == Guid.Empty)
            return 0m;

        await LogTransactionAsync(
            CustomerTransactionType.Overpayment,
            customerId,
            prescriptionId,
            overpayment
        );
        return overpayment;
    }

    public Task LogCashbackEarnedAsync(Guid customerId, Guid prescriptionId, decimal amount) =>
        LogTransactionAsync(
            CustomerTransactionType.CashbackEarned,
            customerId,
            prescriptionId,
            amount
        );

    public Task LogCashbackUsedAsync(Guid customerId, Guid prescriptionId, decimal amount) =>
        LogTransactionAsync(
            CustomerTransactionType.CashbackUsed,
            customerId,
            prescriptionId,
            amount
        );

    public Task LogDebtAsync(Guid customerId, Guid prescriptionId, decimal amount) =>
        LogTransactionAsync(
            CustomerTransactionType.DebtPayment,
            customerId,
            prescriptionId,
            amount
        );

    public Task LogAutoPaymentAsync(Guid customerId, Guid prescriptionId, decimal amount) =>
        LogTransactionAsync(
            CustomerTransactionType.AutoPayment,
            customerId,
            prescriptionId,
            amount
        );

    public Task AddCreditBalanceAsync(Guid customerId, Guid? prescriptionId, decimal amount) =>
        LogTransactionAsync(
            CustomerTransactionType.ManualAdjustment,
            customerId,
            prescriptionId,
            amount
        );
}
