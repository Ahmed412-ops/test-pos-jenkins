using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Helper.Extensions;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Wallet.Queries.GetAll;

public class GetWalletsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) :
    BaseHandler<GetWalletsQuery, Result<PaginationResponse<GetWalletsResponse>>>
{
    private readonly IGenericRepository<Domain.Entities.Wallets.Wallet> _walletRepo =
        unitOfWork.GetRepository<Domain.Entities.Wallets.Wallet>();
    
    public override async Task<Result<PaginationResponse<GetWalletsResponse>>> Handle(
        GetWalletsQuery request,
        CancellationToken cancellationToken
    )
    {
        var query = await _walletRepo.GetAllQueryableAsync(d => !d.Is_Deleted);

        if (!string.IsNullOrWhiteSpace(request.Name))
            query = query.Where(a => a.Name.Contains(request.Name));

        var count = await query.CountAsync(cancellationToken);

        var response = query
            .Select(a => mapper.Map<GetWalletsResponse>(a))
            .Paginate(request)
            .ToList();

        return Result<PaginationResponse<GetWalletsResponse>>.Success(
            new PaginationResponse<GetWalletsResponse> { Data = response, Count = count }
        );
    }   
}
