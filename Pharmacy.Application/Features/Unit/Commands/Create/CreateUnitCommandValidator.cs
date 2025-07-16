using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Commands;

namespace Pharmacy.Application.Features.Unit.Commands.Create;

public class CreateUnitCommandValidator(IUnitOfWork context) : BaseCommandValidator<CreateUnitCommand, Domain.Entities.Unit.Unit>(context)
{
    
}
