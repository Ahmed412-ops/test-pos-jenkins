using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Application.Features.Order.Commands.Create;
using Pharmacy.Domain.Enum;

namespace Pharmacy.Application.Features.Order.Queries.GetById;

public class GetOrderResponse : CommonQueryResponseBase
{
    public required string PurchaseOrderNumber { get; set; }
    public Guid SupplierId { get; set; }
    public required string SupplierName { get; set; }
    public required DateTime OrderDate { get; set; }
    public DateTime? ExpectedDeliveryDate { get; set; }
    public PaymentTerms PaymentTerms { get; set; } = PaymentTerms.Cash;
    public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
    public List<PurchaseOrderItemResponseDto> Items { get; set; } = [];
 
}

public class PurchaseOrderItemResponseDto : PurchaseOrderItemDto
{
    public string MedicineName { get; set; } = "";
    public string MedicineUnit { get; set; } = "";

}