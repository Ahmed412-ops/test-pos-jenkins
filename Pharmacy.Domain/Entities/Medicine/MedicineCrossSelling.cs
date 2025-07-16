namespace Pharmacy.Domain.Entities.Medicine;

public class MedicineCrossSelling : BaseEntity
{
    public Guid MedicineId { get; set; }
    public Medicine Medicine { get; set; } = null!;
    public Guid CrossSellingMedicineId { get; set; }
    public Medicine CrossSellingMedicine { get; set; } = null!;
}
