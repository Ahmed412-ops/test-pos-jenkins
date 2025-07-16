using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.EffectiveMaterial.Category.Queries.GetById;

public class GetEffectiveMaterialCategoryQuery : IRequest<Result<GetEffectiveMaterialCategoryResponse>>
{
    public Guid Id { get; set; }
}
