namespace Pharmacy.Application.Dto.Common.Queries;

public class CommonQueryResponseBase
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? Notes { get; set; }
}
