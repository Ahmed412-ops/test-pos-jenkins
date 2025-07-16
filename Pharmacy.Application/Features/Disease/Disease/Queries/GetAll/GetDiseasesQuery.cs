using MediatR;
using Pharmacy.Application.Dto;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Disease.Disease.Queries.GetAll;

public class GetDiseasesQuery : Pagination, IRequest<Result<PaginationResponse<GetDiseasesResponse>>>
{
    public string? Name { get; set; }    
}
