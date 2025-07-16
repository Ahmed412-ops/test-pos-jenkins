namespace Pharmacy.Domain.Entities.Customers;

public class Address : BaseEntity
{
    public required string StreetName { get; set; }
    public required string City { get; set; }
    public string? District { get; set; }
    public string? BuildingNumber { get; set; }
    public string? FloorNumber { get; set; }
    public string? ApartmentNumber { get; set; }
    public string? Landmark { get; set; }
    public string? Notes { get; set; }

    public string GetFullAddress()
    {
        return string.Join(", ", new[] { StreetName, BuildingNumber, FloorNumber, ApartmentNumber, District, City }
            .Where(value => !string.IsNullOrWhiteSpace(value)));
    }

    public Guid CustomerId { get; set; }
    public virtual Customer? Customer { get; set; }
}
