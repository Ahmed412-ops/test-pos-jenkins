using FluentValidation;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;

namespace Pharmacy.Application.Features.Shifts.Commands.Close;

public class CloseShiftCommandValidator : AbstractValidator<CloseShiftCommand>
{
    private readonly IGenericRepository<Domain.Entities.Wallets.Shift> _shiftRepository;

    public CloseShiftCommandValidator(IUnitOfWork context)
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        _shiftRepository = context.GetRepository<Domain.Entities.Wallets.Shift>();

        RuleFor(x => x.Id).NotEmpty().WithMessage(Messages.ShiftIdRequired);

        RuleForEach(x => x.Wallets)
            .ChildRules(wallet =>
            {
                wallet.RuleFor(w => w.WalletId).NotEmpty().WithMessage(Messages.WalletIdRequired);

                wallet
                    .RuleFor(w => w.ActualClosingBalance)
                    .GreaterThanOrEqualTo(0)
                    .WithMessage(Messages.ActualClosingBalanceMustBeGreaterThanOrEqualToZero);
            });

        RuleFor(x => x)
            .CustomAsync(
                async (Command, context, cancellationToken) =>
                {
                    var shift = await _shiftRepository.FindAsync(
                        x => x.Id == Command.Id,
                        select: x => new
                        {
                            x.Id,
                            x.IsOpen,
                            walletIds = x.ShiftWallets.Select(sw => sw.WalletId),
                        }
                    );

                    if (shift == null)
                        context.AddFailure(Messages.ShiftNotFound);
                    else if (!shift.IsOpen)
                        context.AddFailure(Messages.ShiftAlreadyClosed);
                    else if (Command.Wallets.Any(w => !shift.walletIds.Contains(w.WalletId)))
                        context.AddFailure(Messages.WalletNotFoundInThisShift);
                }
            );
    }
}
