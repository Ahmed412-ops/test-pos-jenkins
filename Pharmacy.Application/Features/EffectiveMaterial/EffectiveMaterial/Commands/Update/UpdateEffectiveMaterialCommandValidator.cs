using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Commands;
using Pharmacy.Application.Features.EffectiveMaterial.EffectiveMaterial.Commands.Create;

namespace Pharmacy.Application.Features.EffectiveMaterial.EffectiveMaterial.Commands.Update;

public class UpdateEffectiveMaterialCommandValidator : UpdateBaseCommandValidator<UpdateEffectiveMaterialCommand, Domain.Entities.EffectiveMaterial.EffectiveMaterial>
{
    public UpdateEffectiveMaterialCommandValidator(IUnitOfWork context)
        : base(context) 
    {
        Include(new CreateEffectiveMaterialCommandValidator(context, false));        
    }
}
