using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Commands;

namespace Pharmacy.Application.Features.Unit.Commands.Update;

public class UpdateUnitCommandValidator(IUnitOfWork context) : UpdateBaseCommandValidator<UpdateUnitCommand, Domain.Entities.Unit.Unit>(context)
{
    
}
