using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Commands;
using Pharmacy.Domain.Entities.EffectiveMaterial;

namespace Pharmacy.Application.Features.EffectiveMaterial.Category.Commands.Create;

public class CreateEffectiveMaterialCategoryCommandValidator(IUnitOfWork context) : BaseCommandValidator<CreateEffectiveMaterialCategoryCommand, EffectiveMaterialCategory>(context)
{
    
}
