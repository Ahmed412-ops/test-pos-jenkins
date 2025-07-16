using Pharmacy.Domain.Enum;

namespace Pharmacy.Application.Features.Wallet.Queries.GetById;

public class GetWalletResponse 
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? Notes { get; set; }  
    public required WalletType Type { get; set; } 
    public required DeductionType DeductionType { get; set; }
    public decimal? DeductionValue { get; set; }
    public bool IsDefaultForCashPayments { get; set; } = false; 
}
