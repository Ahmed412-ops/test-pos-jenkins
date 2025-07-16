namespace Pharmacy.Domain.Entities.Customers;

public class PhoneNumber : BaseEntity
{
    public required string Number { get; set; } 
    public bool IsWhatsApp { get; set; }

    public Guid CustomerId { get; set; }
    public virtual Customer? Customer { get; set; }
}
