using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Commands;

namespace Pharmacy.Application.Features.Uses.Commands.Update;

public class UpdateUseCommandValidator(IUnitOfWork context) : UpdateBaseCommandValidator<UpdateUseCommand, Domain.Entities.Uses.Use>(context)
{
    
}
