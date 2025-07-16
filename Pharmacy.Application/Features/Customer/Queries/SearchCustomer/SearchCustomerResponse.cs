using Pharmacy.Application.Features.Customer.Queries.GetById.GetByIdResponses;

namespace Pharmacy.Application.Features.Customer.Queries.SearchCustomer;

public class SearchCustomerResponse
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
}
