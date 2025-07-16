namespace Pharmacy.Domain.Entities.Customers;

public class CustomerChronicDisease : BaseEntity
{
    public Guid CustomerId { get; set; }
    public virtual Customer Customer { get; set; } = default!;

    public Guid DiseaseId { get; set; }
    public virtual Disease.Disease Disease { get; set; } = default!;
}
