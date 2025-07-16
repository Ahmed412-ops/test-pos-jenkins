using MediatR;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Wallet.Queries.DropDown;

public class WalletsDropDownQuery : IRequest<Result<List<DropDownQueryResponse>>>
{
    
}
