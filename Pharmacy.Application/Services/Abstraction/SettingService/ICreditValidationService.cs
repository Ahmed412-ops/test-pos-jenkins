using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Entities.Customers;
using Pharmacy.Domain.Entities.Wallets.Sales;

namespace Pharmacy.Application.Services.Abstraction.SettingService;
public interface ICreditValidationService
{
    Task<Result<Prescription>> ValidateAsync(Prescription Newprescription);
}
