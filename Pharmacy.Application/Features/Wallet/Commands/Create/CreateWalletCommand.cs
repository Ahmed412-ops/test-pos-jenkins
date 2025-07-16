using MediatR;
using Pharmacy.Application.Dto.Common.Commands;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Enum;

namespace Pharmacy.Application.Features.Wallet.Commands.Create;

public class CreateWalletCommand : IBaseCommand, IRequest<Result<string>>
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public required WalletType Type { get; set; }
    public required DeductionType DeductionType { get; set; }
    public decimal? DeductionValue { get; set; }
    public bool IsDefaultForCashPayments { get; set; } = false;
}
