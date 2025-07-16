using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Commands;

namespace Pharmacy.Application.Features.SideEffect.Commands.Update;

public class UpdateSideEffectCommandValidator(IUnitOfWork context) : UpdateBaseCommandValidator<UpdateSideEffectCommand, Domain.Entities.SideEffects.SideEffect>(context)
{
    
}
