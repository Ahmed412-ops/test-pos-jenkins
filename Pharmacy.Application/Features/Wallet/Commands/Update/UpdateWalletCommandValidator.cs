using FluentValidation;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Commands;
using Pharmacy.Application.Features.Wallet.Commands.Create;
using Pharmacy.Application.Resources.Static;

namespace Pharmacy.Application.Features.Wallet.Commands.Update;

public class UpdateWalletCommandValidator : UpdateBaseCommandValidator<UpdateWalletCommand, Domain.Entities.Wallets.Wallet>
{
    public UpdateWalletCommandValidator(IUnitOfWork unitOfWork)
        : base(unitOfWork)
    {
        Include(new CreateWalletCommandValidator(unitOfWork, false));

        RuleFor(x => x.IsDefaultForCashPayments)
            .MustAsync(async (command, isDefault, cancellationToken) =>
            {
                if (!isDefault)
                    return true;

                bool defaultExists = await unitOfWork
                    .GetRepository<Domain.Entities.Wallets.Wallet>()
                    .IsExistsAsync(wallet =>
                        wallet.IsDefaultForCashPayments && wallet.Id != command.Id);
                return !defaultExists;
            })
            .WithMessage(Messages.DefaultWalletAlreadyExists)
            .When(x => x.IsDefaultForCashPayments);
    }
}
