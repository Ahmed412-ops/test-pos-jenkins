using Pharmacy.Domain.Entities.Wallets.Sales;

namespace Pharmacy.Application.Services.Abstraction.PrescriptionTransactionService;

public interface IPrescriptionTransactionService
{
    Task<decimal> ApplyTransactionAsync(
        Prescription prescription,
        decimal amountPaid,
        Guid shiftWalletId
    );
}
