using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Customer.Queries.SearchCustomer;

public class SearchCustomerQuery : IRequest<Result<List<SearchCustomerResponse>>>
{
    public string? NameOrPhone { get; set; }
}
