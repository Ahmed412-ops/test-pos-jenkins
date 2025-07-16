using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Commands;

namespace Pharmacy.Application.Features.DosageForm.Commands.Update;

public class UpdateDosageFormCommandValidator(IUnitOfWork context) : UpdateBaseCommandValidator<UpdateDosageFormCommand, Domain.Entities.DosageForm.DosageForm>(context)
{
    
}
