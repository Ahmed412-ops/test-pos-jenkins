using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Entities.Wallets.Sales;

namespace Pharmacy.Application.Services.Abstraction.CashBackServices;

public interface ICashbackService
{
    Task<Result<bool>> ValidateCashbackEligibility(Prescription prescription);
    Task<decimal> CalculateEarnedCashback(decimal prescriptionAmount);
}
