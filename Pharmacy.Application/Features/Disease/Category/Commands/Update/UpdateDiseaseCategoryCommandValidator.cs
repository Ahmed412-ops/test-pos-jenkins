using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Commands;
using Pharmacy.Domain.Entities.Disease;

namespace Pharmacy.Application.Features.Disease.Category.Commands.Update;

public class UpdateDiseaseCategoryCommandValidator(IUnitOfWork context) : UpdateBaseCommandValidator<UpdateDiseaseCategoryCommand, DiseaseCategory>(context)
{
    
}
