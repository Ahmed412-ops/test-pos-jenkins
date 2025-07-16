using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Commands;
using Pharmacy.Domain.Entities.Disease;

namespace Pharmacy.Application.Features.Disease.Category.Commands.Create;

public class CreateDiseaseCategoryCommandValidator(IUnitOfWork context) : BaseCommandValidator<CreateDiseaseCategoryCommand, DiseaseCategory>(context)
{
    
}
