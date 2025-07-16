using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Shifts.Commands.Create;

public class OpenShiftCommand : IRequest<Result<string>>
{
    public List<OpenShiftWalletDto> Wallets { get; set; } = [];
}

public class OpenShiftWalletDto
{
    public Guid WalletId { get; set; }
    public decimal OpeningBalance { get; set; }
}
