using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Wallet.Queries.GetById;

public class GetWalletQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : BaseHandler<GetWalletQuery, Result<GetWalletResponse>>
{
    private readonly IGenericRepository<Domain.Entities.Wallets.Wallet> _walletRepo = unitOfWork.GetRepository<Domain.Entities.Wallets.Wallet>();
    public override async Task<Result<GetWalletResponse>> Handle(GetWalletQuery request, CancellationToken cancellationToken)
    {
        var wallet = await _walletRepo.FindAsync(
            s => s.Id == request.Id,
            asNoTracking: true);

        if (wallet == null)
            return Result<GetWalletResponse>.Fail(Messages.NotFound);

        var response = mapper.Map<GetWalletResponse>(wallet);

        return Result<GetWalletResponse>.Success(response);
    }
}
