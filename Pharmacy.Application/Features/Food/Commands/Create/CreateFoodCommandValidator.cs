using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Commands;

namespace Pharmacy.Application.Features.Food.Commands.Create;

public class CreateFoodCommandValidator(IUnitOfWork context) : BaseCommandValidator<CreateFoodCommand, Domain.Entities.Food.Food>(context)
{
    
}
