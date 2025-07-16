using Pharmacy.Application.Dto.Common.Commands;
using Pharmacy.Application.Features.Supplier.Commands.Create;

namespace Pharmacy.Application.Features.Supplier.Commands.Update;

public class UpdateSupplierCommand : CreateSupplierCommand, IBaseUpdateCommand
{
    public Guid Id { get; set; }
}
