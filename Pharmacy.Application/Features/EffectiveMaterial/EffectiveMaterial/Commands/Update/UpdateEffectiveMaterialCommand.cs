using Pharmacy.Application.Dto.Common.Commands;
using Pharmacy.Application.Features.EffectiveMaterial.EffectiveMaterial.Commands.Create;

namespace Pharmacy.Application.Features.EffectiveMaterial.EffectiveMaterial.Commands.Update;

public class UpdateEffectiveMaterialCommand : CreateEffectiveMaterialCommand, IBaseUpdateCommand
{
    public Guid Id { get; set; }
}
