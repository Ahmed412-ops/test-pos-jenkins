using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Wallet.Queries.GetById;

public class GetWalletQuery : IRequest<Result<GetWalletResponse>>
{
    public Guid Id { get; set; }
}
