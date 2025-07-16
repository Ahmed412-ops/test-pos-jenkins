using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Commands;

namespace Pharmacy.Application.Features.DosageForm.Commands.Create;

public class CreateDosageFormCommandValidator(IUnitOfWork context) : BaseCommandValidator<CreateDosageFormCommand, Domain.Entities.DosageForm.DosageForm>(context)
{
    
}
