using MediatR;
using Pharmacy.Application.Dto;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Wallet.Queries.GetAll;

public class GetWalletsQuery : Pagination, IRequest<Result<PaginationResponse<GetWalletsResponse>>>
{
    public string? Name { get; set; }
}
