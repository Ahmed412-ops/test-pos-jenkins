using Pharmacy.Application.Dto.Common.Commands;
using Pharmacy.Application.Features.Order.Commands.Create;

namespace Pharmacy.Application.Features.Order.Commands.Update;

public class UpdateOrderCommand : CreateOrderCommand, IBaseUpdateCommand
{
    public Guid Id { get; set; }
}
