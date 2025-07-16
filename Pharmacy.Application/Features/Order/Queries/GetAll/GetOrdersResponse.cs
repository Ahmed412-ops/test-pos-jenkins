using Pharmacy.Application.Dto.Common.Queries;

namespace Pharmacy.Application.Features.Order.Queries.GetAll;

public class GetOrdersResponse : CommonQueryResponseBase
{
    public required string PurchaseOrderNumber { get; set; }
    public required string OrderStatus { get; set; }
    public required DateTime OrderDate { get; set; }
}
