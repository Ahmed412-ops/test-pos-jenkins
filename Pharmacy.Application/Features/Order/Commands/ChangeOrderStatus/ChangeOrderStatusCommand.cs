using MediatR;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Enum;

namespace Pharmacy.Application.Features.Order.Commands.ChangeOrderStatus;

public class ChangeOrderStatusCommand : IRequest<Result<bool>>
{
    public Guid Id { get; set; }
    public OrderStatus OrderStatus { get; set; }
}
