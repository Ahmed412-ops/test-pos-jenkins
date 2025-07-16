using MediatR;
using Pharmacy.Application.Dto;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.EffectiveMaterial.Category.Queries.GetAll;

public class GetEffectiveMaterialCategoriesQuery : Pagination, IRequest<Result<PaginationResponse<GetEffectiveMaterialCategoriesResponse>>>
{
    public string? Name { get; set; }
}

