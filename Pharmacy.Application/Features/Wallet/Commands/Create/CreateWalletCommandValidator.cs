using FluentValidation;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Commands;
using Pharmacy.Application.Resources.Static;

namespace Pharmacy.Application.Features.Wallet.Commands.Create;

public class CreateWalletCommandValidator
    : BaseCommandValidator<CreateWalletCommand, Domain.Entities.Wallets.Wallet>
{
    public CreateWalletCommandValidator(IUnitOfWork unitOfWork, bool checkName = true)
        : base(unitOfWork, checkName)
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Type).IsInEnum().WithMessage(Messages.InvalidWalletType);

        RuleFor(x => x.DeductionType).IsInEnum().WithMessage(Messages.InvalidDeductionType);

        RuleFor(x => x.DeductionValue)
            .GreaterThanOrEqualTo(0)
            .WithMessage(Messages.QuantityMustBeGreaterThanZero);

        RuleFor(x => x.IsDefaultForCashPayments)
            .MustAsync(
                async (command, isDefault, cancellationToken) =>
                {
                    // Only perform the check when the incoming wallet is flagged as default.
                    if (!isDefault)
                        return true;

                    bool defaultExists = await unitOfWork
                        .GetRepository<Domain.Entities.Wallets.Wallet>()
                        .IsExistsAsync(wallet => wallet.IsDefaultForCashPayments);
                    return !defaultExists;
                }
            )
            .WithMessage(Messages.DefaultWalletAlreadyExists);
    }
}
