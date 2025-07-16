namespace Pharmacy.Application.Services.Abstraction.CustomerBalanceService;

public interface ICustomerWalletService
{
    Task LogNormalPaymentAsync(Guid customerId, Guid prescriptionId, decimal amount);
    Task LogCreditUsageAsync(Guid customerId, Guid prescriptionId, decimal amount);
    Task<decimal> RecordOverpaymentAsync(
        Guid customerId,
        Guid prescriptionId,
        decimal paidAmount,
        decimal dueAmount
    );
    Task LogCashbackEarnedAsync(Guid customerId, Guid prescriptionId, decimal amount);
    Task LogCashbackUsedAsync(Guid customerId, Guid prescriptionId, decimal amount);
    Task LogDebtAsync(Guid customerId, Guid prescriptionId, decimal amount);
    Task LogAutoPaymentAsync(Guid customerId, Guid prescriptionId, decimal amount);
    Task AddCreditBalanceAsync(Guid customerId, Guid? prescriptionId, decimal amount);
}
