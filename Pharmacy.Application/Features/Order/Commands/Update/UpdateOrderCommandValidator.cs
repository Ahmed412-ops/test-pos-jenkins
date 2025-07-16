using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Commands;
using Pharmacy.Application.Features.Order.Commands.Create;

namespace Pharmacy.Application.Features.Order.Commands.Update;

public class UpdateOrderCommandValidator : UpdateBaseCommandValidator<UpdateOrderCommand, Domain.Entities.Order.PurchaseOrder>
{
    public UpdateOrderCommandValidator(IUnitOfWork context)
        : base(context)
    {
        Include(new CreateOrderCommandValidator(context, false));
    }
}
