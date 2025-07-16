using MediatR;
using Pharmacy.Application.Dto;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.EffectiveMaterial.EffectiveMaterial.Queries.GetAll;

public class GetEffectiveMaterialsQuery : Pagination, IRequest<Result<PaginationResponse<GetEffectiveMaterialsResponse>>>
{
    public string? Name { get; set; }
}
