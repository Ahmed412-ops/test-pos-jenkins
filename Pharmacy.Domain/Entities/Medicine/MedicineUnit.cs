namespace Pharmacy.Domain.Entities.Medicine;

public class MedicineUnit : BaseEntity
{
    public Guid? MedicineId { get; set; }
    public virtual Medicine? Medicine { get; set; }
    public Guid UnitId { get; set; }
    public virtual Unit.Unit Unit { get; set; } = null!;
    public bool CanBeSold { get; set; } = true;
    public bool IsDefault { get; set; } = false;
    public bool CalcUnit { get; set; } = false;
    public decimal QuantityForCalcUnit { get; set; } = 1;
}
