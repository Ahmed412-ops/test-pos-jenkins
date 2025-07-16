using MediatR;
using Pharmacy.Application.Dto;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Unit.Queries.GetAll;

public class GetUnitsQuery : Pagination, IRequest<Result<PaginationResponse<GetUnitsResponse>>>
{
    public string? Name { get; set; }
}
