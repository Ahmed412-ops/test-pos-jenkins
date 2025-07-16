using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Enum;

namespace Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Commands.AddTransaction;

public class AddTransactionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<AddTransactionCommand, Result<string>>
{
    private readonly IGenericRepository<Domain.Entities.SupplierTransaction.SupplierTransaction> _transactionRepo
     = unitOfWork.GetRepository<Domain.Entities.SupplierTransaction.SupplierTransaction>();
    private readonly IGenericRepository<Domain.Entities.SupplierInvoice.SupplierInvoice> _supplierInvoiceRepository
     = unitOfWork.GetRepository<Domain.Entities.SupplierInvoice.SupplierInvoice>();
    private readonly IGenericRepository<Domain.Entities.Wallets.ShiftWallet> _shiftWalletRepository
        = unitOfWork.GetRepository<Domain.Entities.Wallets.ShiftWallet>();
    public override async Task<Result<string>> Handle(
        AddTransactionCommand request,
        CancellationToken cancellationToken
    )
    {
        var supplierInvoice = await _supplierInvoiceRepository
        .FindAsync(s => s.Id == request.SupplierInvoiceId);
        if (supplierInvoice == null)
            return Result<string>.Fail(Messages.SupplierInvoiceNotFound);

        if (request.Amount > supplierInvoice.RemainingBalance)
            return Result<string>.Fail(Messages.PaymentExceedsRemainingBalance);

        var shiftWallet = await _shiftWalletRepository.
        FindAsync(sw => sw.Id == request.ShiftWalletId
        , Include: i => i.Include(a => a.Shift));
        if (shiftWallet == null || shiftWallet.Shift.ClosedAt != null)
            return Result<string>.Fail(Messages.RequiredOpenShift);

        var transaction = mapper.Map<Domain.Entities.SupplierTransaction.SupplierTransaction>(request);
        transaction.ShiftWalletId = shiftWallet?.Id;
        transaction.PaymentMethod = request.PaymentMethod;

        supplierInvoice.AmountPaid += request.Amount;
        if (supplierInvoice.RemainingBalance == 0)
            supplierInvoice.PaymentStatus = Domain.Enum.PaymentStatus.Paid;
        else if (supplierInvoice.AmountPaid > 0)
            supplierInvoice.PaymentStatus = PaymentStatus.PartiallyPaid;

        await _transactionRepo.AddAsync(transaction);
        await unitOfWork.SaveChangesAsync();
        return Result<string>.Success(Messages.SuccessfullyCreated);
    }
}
