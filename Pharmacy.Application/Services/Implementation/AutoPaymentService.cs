using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Services.Abstraction.AutoPaymentService;
using Pharmacy.Application.Services.Abstraction.CustomerBalanceService;
using Pharmacy.Domain.Entities.Customers;
using Pharmacy.Domain.Entities.Wallets.Sales;

namespace Pharmacy.Application.Services.Implementation;

public class AutoPaymentService(IUnitOfWork unitOfWork, ICustomerWalletService walletService)
    : IAutoPaymentService
{
    private readonly IGenericRepository<Prescription> _prescriptionRepository =
        unitOfWork.GetRepository<Prescription>();

    private readonly IGenericRepository<Customer> _customerRepository =
        unitOfWork.GetRepository<Customer>();

    public async Task<PaymentDistributionResult> DistributePaymentAsync(
        Guid customerId,
        Guid shiftWalletId,
        decimal paymentAmount
    )
    {
        var customer = await _customerRepository.FindAsync(
            c => c.Id == customerId,
            Include: c => c.Include(cu => cu.BalanceTransactions)
        );
        decimal existingCredit = customer!.CreditBalance;

        var result = new PaymentDistributionResult(new List<AppliedPayment>(), 0m);

        if (paymentAmount <= 0)
        {
            result.RemainingCredit = existingCredit;
            return result;
        }

        var query = await _prescriptionRepository.GetAllQueryableAsync(
            p => p.CustomerId == customerId && p.PaymentStatus != PaymentStatus.FullyPaid,
            orderBy: q => q.OrderBy(p => p.Created_At),
            Include: q =>
                q.Include(p => p.Transactions)
                    .Include(p => p.Items)
                    .ThenInclude(i => i.MedicationStock)
        );
        var prescriptions = await query.ToListAsync();

        foreach (var prescription in prescriptions)
        {
            if (paymentAmount <= 0)
                break;

            decimal balanceDue = prescription.AmountDue;
            if (balanceDue <= 0)
                continue;

            decimal amountToApply = Math.Min(balanceDue, paymentAmount);

            var tx = new PrescriptionTransaction
            {
                PrescriptionId = prescription.Id,
                ShiftWalletId = shiftWalletId,
                AmountPaid = amountToApply,
            };
            prescription.Transactions.Add(tx);

            decimal newBalance = balanceDue - amountToApply;
            prescription.PaymentStatus =
                newBalance > 0 ? PaymentStatus.PartiallyPaid : PaymentStatus.FullyPaid;

            await walletService.LogAutoPaymentAsync(customerId, prescription.Id, amountToApply);

            paymentAmount -= amountToApply;

            result.AppliedPayments.Add(
                new AppliedPayment(prescription.Id, amountToApply, newBalance)
            );
        }

        decimal newCredit = 0m;
        if (paymentAmount > 0)
        {
            var firstPrescriptionId = prescriptions.FirstOrDefault()?.Id;
            await walletService.AddCreditBalanceAsync(
                customerId,
                firstPrescriptionId,
                paymentAmount
            );
            newCredit = paymentAmount;
        }

        await unitOfWork.SaveChangesAsync();

        result.RemainingCredit = existingCredit + newCredit;
        return result;
    }
}
