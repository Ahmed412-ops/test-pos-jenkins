using Pharmacy.Application.Dto.Common.Commands;
using Pharmacy.Application.Features.Wallet.Commands.Create;

namespace Pharmacy.Application.Features.Wallet.Commands.Update;

public class UpdateWalletCommand : CreateWalletCommand, IBaseUpdateCommand
{
    public Guid Id { get; set; }
}
