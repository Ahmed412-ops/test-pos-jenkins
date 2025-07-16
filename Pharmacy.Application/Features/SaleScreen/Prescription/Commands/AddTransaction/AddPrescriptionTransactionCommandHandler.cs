using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Application.Services.Abstraction.CustomerBalanceService;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Entities.Wallets.Sales;

namespace Pharmacy.Application.Features.SaleScreen.Prescription.Commands.AddTransaction;

public class AddPrescriptionTransactionCommandHandler(
    IUnitOfWork unitOfWork,
    ICustomerWalletService walletService
) : BaseHandler<AddPrescriptionTransactionCommand, Result<AddPrescriptionTransactionResponse>>
{
    private readonly IGenericRepository<Domain.Entities.Wallets.Sales.Prescription> _prescriptionRepository =
        unitOfWork.GetRepository<Domain.Entities.Wallets.Sales.Prescription>();

    public override async Task<Result<AddPrescriptionTransactionResponse>> Handle(
        AddPrescriptionTransactionCommand request,
        CancellationToken cancellationToken
    )
    {
        var prescription = await _prescriptionRepository.FindAsync(
            s => s.Id == request.PrescriptionId,
            Include: s =>
                s.Include(p => p.Transactions)
                    .Include(p => p.Items)
                    .ThenInclude(i => i.MedicationStock)
                    .ThenInclude(ms => ms.Medicine)
                    .Include(p => p.Transactions),
            asNoTracking: false
        );
        if (prescription == null)
            return Result<AddPrescriptionTransactionResponse>.Fail(Messages.PrescriptionNotFound);

        if (prescription.CustomerId == null)
            return Result<AddPrescriptionTransactionResponse>.Fail(Messages.CustomerNotFound);

        if (prescription.AmountDue <= 0)
            return Result<AddPrescriptionTransactionResponse>.Fail(
                Messages.PrescriptionAlreadyPaid
            );

        if (request.AmountPaid > prescription.AmountDue)
            return Result<AddPrescriptionTransactionResponse>.Fail(
                Messages.PrescriptionOverpayment
            );
        if (request.AmountPaid <= 0)
            return Result<AddPrescriptionTransactionResponse>.Fail(Messages.InvalidAmountPaid);

        var newTransaction = new PrescriptionTransaction
        {
            PrescriptionId = prescription.Id,
            ShiftWalletId = request.ShiftWalletId,
            AmountPaid = request.AmountPaid,
        };

        prescription.Transactions.Add(newTransaction);
        await walletService.LogDebtAsync(
            prescription.CustomerId.Value,
            prescription.Id,
            request.AmountPaid
        );
        await unitOfWork.SaveChangesAsync();

        return Result<AddPrescriptionTransactionResponse>.Success(
            new AddPrescriptionTransactionResponse
            {
                PrescriptionId = prescription.Id,
                InvoiceNumber = newTransaction.InvoiceNumber,
                AmountPaid = request.AmountPaid,
                AmountDue = prescription.AmountDue,
                Created_At = newTransaction.Created_At,
            }
        );
    }
}
