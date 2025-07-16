using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Commands.DeleteTransaction;

public class DeleteTransactionCommandHandler(IUnitOfWork unitOfWork)
    : BaseHandler<DeleteTransactionCommand, Result<bool>>
{
    private readonly IGenericRepository<Domain.Entities.SupplierTransaction.SupplierTransaction> _transactionRepo =
        unitOfWork.GetRepository<Domain.Entities.SupplierTransaction.SupplierTransaction>();

    public override async Task<Result<bool>> Handle(
        DeleteTransactionCommand request,
        CancellationToken cancellationToken
    )
    {
        var transaction = await _transactionRepo.FindAsync(
            t => t.Id == request.Id,
            Include: t => t.Include(t => t.SupplierInvoice!)
        );

        if (transaction == null)
            return Result<bool>.Fail(Messages.NotFound);

        transaction.SupplierInvoice!.AmountPaid -= transaction.Amount;
        transaction.Is_Deleted = true;

        int result = await unitOfWork.SaveChangesAsync();
        if (result <= 0)
            return Result<bool>.Fail(Messages.SomethingWentWrong);

        return Result<bool>.Success(true, Messages.DeletedSuccessfully);
    }
}
