using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Commands;
using Pharmacy.Application.Features.Supplier.Commands.Create;

namespace Pharmacy.Application.Features.Supplier.Commands.Update;

public class UpdateSupplierCommandValidator : UpdateBaseCommandValidator<UpdateSupplierCommand, Domain.Entities.Supplier.Supplier>
{
    public UpdateSupplierCommandValidator(IUnitOfWork context)
        : base(context)
    {
        Include(new CreateSupplierCommandValidator(context, false));
    }
}
