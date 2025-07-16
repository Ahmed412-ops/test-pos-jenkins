using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Wallet.Commands.Create;

public class CreateWalletCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : BaseHandler<CreateWalletCommand, Result<string>>
{
    private readonly IGenericRepository<Domain.Entities.Wallets.Wallet> _walletRepository = unitOfWork.GetRepository<Domain.Entities.Wallets.Wallet>();
    public override async Task<Result<string>> Handle(CreateWalletCommand request, CancellationToken cancellationToken)
    {
        var wallet = mapper.Map<Domain.Entities.Wallets.Wallet>(request);
        await _walletRepository.AddAsync(wallet);
        await unitOfWork.SaveChangesAsync();
        return Result<string>.Success(Messages.SuccessfullyCreated);
    }
}
