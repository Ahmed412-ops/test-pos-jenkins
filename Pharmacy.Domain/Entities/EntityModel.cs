namespace Pharmacy.Domain.Entities;

public class EntityModel : BaseEntity
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? Notes { get; set; }
}
