using FluentValidation;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Helper.Extensions;
using Pharmacy.Application.Resources.Static;

namespace Pharmacy.Application.Features.Shifts.Commands.Create;

public class OpenShiftCommandValidator : AbstractValidator<OpenShiftCommand>
{
    public OpenShiftCommandValidator(IUnitOfWork context)
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Wallets).NotEmpty().WithMessage(Messages.AtLeastOneWalletRequired);

        RuleForEach(x => x.Wallets)
            .ChildRules(wallet =>
            {
                wallet
                    .RuleFor(w => w.WalletId)
                    .NotEmpty()
                    .WithMessage(Messages.WalletIdRequired)
                    .MustExistWallet(context)
                    .MustNotInOpenShift(context);

                wallet
                    .RuleFor(w => w.OpeningBalance)
                    .GreaterThanOrEqualTo(0)
                    .WithMessage(Messages.OpeningBalanceMustBeGreaterThanOrEqualToZero);
            });
    }
}
