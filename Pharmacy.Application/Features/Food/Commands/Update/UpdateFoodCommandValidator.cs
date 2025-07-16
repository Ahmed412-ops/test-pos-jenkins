using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Commands;

namespace Pharmacy.Application.Features.Food.Commands.Update;

public class UpdateFoodCommandValidator(IUnitOfWork context) : UpdateBaseCommandValidator<UpdateFoodCommand, Domain.Entities.Food.Food>(context)
{
    
}
