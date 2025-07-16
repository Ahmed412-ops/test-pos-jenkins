using MediatR;
using Pharmacy.Application.Dto;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Uses.Queries.GetAll;

public class GetUsesQuery : Pagination, IRequest<Result<PaginationResponse<GetUsesResponse>>>
{
    public string? Name { get; set; }
}
