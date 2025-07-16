using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Commands;

namespace Pharmacy.Application.Features.Uses.Commands.Create;

public class CreateUseCommandValidator(IUnitOfWork context) : BaseCommandValidator<CreateUseCommand, Domain.Entities.Uses.Use>(context)
{
    
}
