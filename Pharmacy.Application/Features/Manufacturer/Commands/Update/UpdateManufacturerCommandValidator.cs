using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Commands;

namespace Pharmacy.Application.Features.Manufacturer.Commands.Update;

public class UpdateManufacturerCommandValidator(IUnitOfWork context) : UpdateBaseCommandValidator<UpdateManufacturerCommand, Domain.Entities.Manufacturers.Manufacturer>(context)
{
    
}
