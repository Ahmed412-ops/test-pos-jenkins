using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Commands;
using Pharmacy.Domain.Entities.EffectiveMaterial;

namespace Pharmacy.Application.Features.EffectiveMaterial.Category.Commands.Update;

public class UpdateEffectiveMaterialCategoryCommandValidator(IUnitOfWork context) : UpdateBaseCommandValidator<UpdateEffectiveMaterialCategoryCommand, EffectiveMaterialCategory>(context)
{
    
}
