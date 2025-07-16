namespace Pharmacy.Application.Features.Order.Queries.DropDown;

public class OrdersDropDownQueryResponse
{
    public Guid Id { get; set; }
    public required string PurchaseOrderNumber { get; set; }

}
