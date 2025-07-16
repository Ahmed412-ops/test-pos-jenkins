using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Commands;

namespace Pharmacy.Application.Features.Symptom.Commands.Update;

public class UpdateSymptomCommandValidator(IUnitOfWork context) : UpdateBaseCommandValidator<UpdateSymptomCommand, Domain.Entities.Symptoms.Symptom>(context)
{
    
}
