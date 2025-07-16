namespace Pharmacy.Domain.Entities.Customers;

public class CustomerChronicMedicine : BaseEntity
{
    public Guid CustomerId { get; set; }
    public virtual Customer Customer { get; set; } = default!;

    public Guid MedicineId { get; set; }
    public virtual Medicine.Medicine Medicine { get; set; } = default!;
}
