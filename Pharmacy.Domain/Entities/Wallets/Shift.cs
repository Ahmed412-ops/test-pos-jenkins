using Pharmacy.Domain.Entities.Identity;
using Pharmacy.Domain.Entities.Wallets.Sales;

namespace Pharmacy.Domain.Entities.Wallets;

public class Shift : BaseEntity
{
    public Guid OpenedById { get; set; }
    public ApplicationUser OpenedBy { get; set; } = null!;
    public DateTime OpenedAt { get; set; } = DateTime.UtcNow;
    public Guid? ClosedById { get; set; }
    public ApplicationUser? ClosedBy { get; set; }
    public DateTime? ClosedAt { get; set; }
    public bool IsOpen => ClosedAt == null;
    public string? Notes { get; set; }
    public decimal OpeningBalance => ShiftWallets.Sum(x => x.OpeningBalance);
    public decimal IncomingTotal => ShiftWallets.Sum(x => x.TotalSales);
    public decimal OutgoingTotal => ShiftWallets.Sum(x => x.TotalExpenses);
    public decimal NetCashMovement => IncomingTotal - OutgoingTotal;
    public decimal ExpectedClosingBalanceTotal => OpeningBalance + IncomingTotal - OutgoingTotal;
    public decimal? ActualClosingBalanceTotal => ShiftWallets.Sum(x => x.ActualClosingBalance);
    public decimal? Difference => ActualClosingBalanceTotal - ExpectedClosingBalanceTotal;
    public ICollection<ShiftWallet> ShiftWallets { get; set; } = [];
    public ICollection<Prescription> Prescriptions { get; set; } = [];
}
