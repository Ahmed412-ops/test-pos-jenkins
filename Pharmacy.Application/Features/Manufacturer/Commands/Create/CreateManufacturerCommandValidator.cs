using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Commands;

namespace Pharmacy.Application.Features.Manufacturer.Commands.Create;

public class CreateManufacturerCommandValidator(IUnitOfWork context) : BaseCommandValidator<CreateManufacturerCommand, Domain.Entities.Manufacturers.Manufacturer>(context)
{
    
}
