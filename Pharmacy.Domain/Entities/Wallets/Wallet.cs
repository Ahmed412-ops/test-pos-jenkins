using Pharmacy.Domain.Enum;

namespace Pharmacy.Domain.Entities.Wallets;

public class Wallet : EntityModel
{
    public WalletType Type { get; set; }
    public DeductionType DeductionType { get; set; }
    public decimal? DeductionValue { get; set; }
    public bool IsDefaultForCashPayments { get; set; } = false;
    public ICollection<ShiftWallet> ShiftWallets { get; set; } = [];
}
