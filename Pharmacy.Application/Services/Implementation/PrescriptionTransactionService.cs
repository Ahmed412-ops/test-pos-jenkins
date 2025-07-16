using Pharmacy.Application.Services.Abstraction.CustomerBalanceService;
using Pharmacy.Application.Services.Abstraction.PrescriptionTransactionService;
using Pharmacy.Domain.Entities.Wallets.Sales;

namespace Pharmacy.Application.Services.Implementation;

public class PrescriptionPaymentService(ICustomerWalletService walletService)
    : IPrescriptionTransactionService
{
    public async Task<decimal> ApplyTransactionAsync(
        Prescription prescription,
        decimal amountPaid,
        Guid shiftWalletId
    )
    {
        decimal dueBefore = prescription.AmountDue;

        decimal overpay = amountPaid - dueBefore;

        prescription.Transactions.Add(
            new PrescriptionTransaction { ShiftWalletId = shiftWalletId, AmountPaid = amountPaid }
        );

        var newBalance = dueBefore - amountPaid;
        prescription.PaymentStatus =
            newBalance > 0 ? PaymentStatus.PartiallyPaid : PaymentStatus.FullyPaid;

        if (dueBefore > 0)
        {
            var applied = Math.Min(dueBefore, amountPaid);
            await walletService.LogNormalPaymentAsync(
                prescription.CustomerId!.Value,
                prescription.Id,
                applied
            );
        }

        if (overpay > 0)
        {
            await walletService.RecordOverpaymentAsync(
                prescription.CustomerId!.Value,
                prescription.Id,
                amountPaid,
                dueBefore
            );
        }

        return overpay;
    }
}
