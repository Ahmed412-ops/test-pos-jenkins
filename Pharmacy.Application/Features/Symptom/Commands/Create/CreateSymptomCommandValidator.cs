using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Commands;

namespace Pharmacy.Application.Features.Symptom.Commands.Create;

public class CreateSymptomCommandValidator(IUnitOfWork context) : BaseCommandValidator<CreateSymptomCommand, Domain.Entities.Symptoms.Symptom>(context)
{
    
}
