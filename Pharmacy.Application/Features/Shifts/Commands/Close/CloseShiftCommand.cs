using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Shifts.Commands.Close;

public class CloseShiftCommand : IRequest<Result<string>>
{
    public Guid Id { get; set; }
    public string? Notes { get; set; } = null;
    public List<CloseShiftWalletDto> Wallets { get; set; } = [];
}

public class CloseShiftWalletDto
{
    public Guid WalletId { get; set; }
    public decimal ActualClosingBalance { get; set; }
    public string? DifferenceReason { get; set; } = null;
}
