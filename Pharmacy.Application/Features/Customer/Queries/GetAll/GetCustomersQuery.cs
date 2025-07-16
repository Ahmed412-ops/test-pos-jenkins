using MediatR;
using Pharmacy.Application.Dto;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Customer.Queries.GetAll;

public class GetCustomersQuery : Pagination, IRequest<Result<PaginationResponse<GetCustomersResponse>>>
{
    public string? Name { get; set; }
}
