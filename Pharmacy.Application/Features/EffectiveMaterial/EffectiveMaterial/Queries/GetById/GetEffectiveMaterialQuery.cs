using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.EffectiveMaterial.EffectiveMaterial.Queries.GetById;

public class GetEffectiveMaterialQuery : IRequest<Result<GetEffectiveMaterialResponse>>
{
    public Guid Id { get; set; }
}
