using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Wallet.Commands.Update;

public class UpdateWalletCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
 : BaseHandler<UpdateWalletCommand, Result<string>>
{
    private readonly IGenericRepository<Domain.Entities.Wallets.Wallet> _walletRepository = unitOfWork.GetRepository<Domain.Entities.Wallets.Wallet>();
    public override async Task<Result<string>> Handle(UpdateWalletCommand request, CancellationToken cancellationToken)
    {
        var wallet = await _walletRepository.FindAsync(
            s => s.Id == request.Id);

        if (wallet == null)
            return Result<string>.Fail(Messages.WalletNotFound);

        mapper.Map(request, wallet);
        await unitOfWork.SaveChangesAsync();
        return Result<string>.Success(Messages.SuccessfullyUpdated);
    }
}
