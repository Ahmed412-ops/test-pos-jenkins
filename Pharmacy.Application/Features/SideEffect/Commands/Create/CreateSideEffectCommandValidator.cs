using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Commands;

namespace Pharmacy.Application.Features.SideEffect.Commands.Create;

public class CreateSideEffectCommandValidator(IUnitOfWork context) : BaseCommandValidator<CreateSideEffectCommand, Domain.Entities.SideEffects.SideEffect>(context)
{
    
}
