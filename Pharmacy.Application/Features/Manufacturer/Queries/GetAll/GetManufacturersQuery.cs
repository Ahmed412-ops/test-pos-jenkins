using MediatR;
using Pharmacy.Application.Dto;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Manufacturer.Queries.GetAll;

public class GetManufacturersQuery : Pagination, IRequest<Result<PaginationResponse<GetManufacturersResponse>>>
{
    public string? Name { get; set; }
}

