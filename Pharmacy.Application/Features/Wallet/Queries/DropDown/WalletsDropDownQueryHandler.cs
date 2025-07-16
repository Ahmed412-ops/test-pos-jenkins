using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Wallet.Queries.DropDown;

public class WalletsDropDownQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<WalletsDropDownQuery, Result<List<DropDownQueryResponse>>>
{
    private readonly IGenericRepository<Domain.Entities.Wallets.Wallet> _walletRepo = unitOfWork.GetRepository<Domain.Entities.Wallets.Wallet>();
    public override async Task<Result<List<DropDownQueryResponse>>> Handle(
        WalletsDropDownQuery request,
        CancellationToken cancellationToken)
    {
        var wallets = await _walletRepo.GetAllAsync(d => !d.Is_Deleted);
        var result = mapper.Map<List<DropDownQueryResponse>>(wallets);
        return Result<List<DropDownQueryResponse>>.Success(result);
    }
}
