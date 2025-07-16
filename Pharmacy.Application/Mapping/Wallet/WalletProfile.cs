using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Application.Features.Wallet.Commands.Create;
using Pharmacy.Application.Features.Wallet.Commands.Update;
using Pharmacy.Application.Features.Wallet.Queries.GetAll;
using Pharmacy.Application.Features.Wallet.Queries.GetById;

namespace Pharmacy.Application.Mapping.Wallet;

public class WalletProfile : MappingProfileBase
{
    public WalletProfile()
    {
        CreateMap<CreateWalletCommand, Domain.Entities.Wallets.Wallet>();
        CreateMap<UpdateWalletCommand, Domain.Entities.Wallets.Wallet>()
            .IncludeBase<CreateWalletCommand, Domain.Entities.Wallets.Wallet>();

        CreateMap<Domain.Entities.Wallets.Wallet, GetWalletsResponse>();
        CreateMap<Domain.Entities.Wallets.Wallet, GetWalletResponse>();
        CreateMap<Domain.Entities.Wallets.Wallet, DropDownQueryResponse>();
    }
}
