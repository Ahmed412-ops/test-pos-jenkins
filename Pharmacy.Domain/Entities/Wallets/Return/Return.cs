using Pharmacy.Domain.Entities.Wallets.Sales;

namespace Pharmacy.Domain.Entities.Wallets.Return;

public class Return : BaseEntity
{
    public Guid PrescriptionId { get; set; }
    public Prescription Prescription { get; set; } = null!;
    public Guid ShiftWalletId { get; set; }
    public ShiftWallet ShiftWallet { get; set; } = null!;
    public string? Notes { get; set; }
    public decimal TotalRefunded => Items.Sum(x => x.AmountRefunded);
    public ICollection<ReturnItem> Items { get; set; } = [];
}
