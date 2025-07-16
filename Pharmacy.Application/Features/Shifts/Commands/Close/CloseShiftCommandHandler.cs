using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Shifts.Commands.Close;

public class CloseShiftCommandHandler(IUnitOfWork unitOfWork, ICurrentUser currentUser)
    : BaseHandler<CloseShiftCommand, Result<string>>
{
    private readonly IGenericRepository<Domain.Entities.Wallets.Shift> _shiftRepository =
        unitOfWork.GetRepository<Domain.Entities.Wallets.Shift>();

    public override async Task<Result<string>> Handle(
        CloseShiftCommand request,
        CancellationToken cancellationToken
    )
    {
        var shift = await _shiftRepository.FindAsync(
            s => s.Id == request.Id,
            Include: s => s.Include(i => i.ShiftWallets).ThenInclude(m => m.Wallet),
            asNoTracking: false
        );
        if (shift == null)
            return Result<string>.Fail(Messages.ShiftNotFound);

        shift.Notes = request.Notes;
        shift.ClosedAt = DateTime.UtcNow;
        shift.ClosedById = currentUser.GetUserId();
        foreach (var wallet in shift.ShiftWallets)
        {
            var requestWallet = request.Wallets.FirstOrDefault(w => w.WalletId == wallet.WalletId);
            if (requestWallet != null)
            {
                wallet.ActualClosingBalance = requestWallet.ActualClosingBalance;
                wallet.DifferenceReason = requestWallet.DifferenceReason;
            }
        }

        await unitOfWork.SaveChangesAsync();

        return Result<string>.Success(Messages.ShiftSuccessfullyClosed);
    }
}
