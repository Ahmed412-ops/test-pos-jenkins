using Pharmacy.Domain.Entities.Wallets.Sales;

namespace Pharmacy.Domain.Entities.Wallets.Return;

public class ReturnItem : BaseEntity
{
    public Guid ReturnId { get; set; }
    public Return Return { get; set; } = null!;
    public Guid PrescriptionItemId { get; set; }
    public PrescriptionItem PrescriptionItem { get; set; } = null!;
    public int QuantityReturned { get; set; }
    public decimal AmountRefunded { get; set; }
    public bool IsDamaged { get; set; }
    public string Reason { get; set; } = string.Empty;
}
