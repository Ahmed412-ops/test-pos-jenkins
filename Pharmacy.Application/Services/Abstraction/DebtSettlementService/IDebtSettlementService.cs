namespace Pharmacy.Application.Services.Abstraction.DebtSettlementService;

public interface IDebtSettlementService
{
    Task<decimal> SettleCustomerDebtsAsync(Guid customerId);
}
