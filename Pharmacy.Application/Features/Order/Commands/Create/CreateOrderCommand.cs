using MediatR;
using Pharmacy.Application.Dto.Common.Commands;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Enum;

namespace Pharmacy.Application.Features.Order.Commands.Create;

public class CreateOrderCommand : IBaseCommand, IRequest<Result<string>>
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public Guid SupplierId { get; set; }
    public required DateTime OrderDate { get; set; }
    public DateTime? ExpectedDeliveryDate { get; set; }
    public PaymentTerms PaymentTerms { get; set; } = PaymentTerms.Cash;
    public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
    public List<PurchaseOrderItemDto> Items { get; set; } = [];
}

public class PurchaseOrderItemDto
{
    public Guid MedicineUnitId { get; set; }
    public decimal Quantity { get; set; }
}