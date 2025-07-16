using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.EffectiveMaterial.EffectiveMaterial.Commands.Delete;

public class DeleteEffectiveMaterialCommand : IRequest<Result<bool>>
{
    public Guid Id { get; set; }
}
