using MediatR;
using Pharmacy.Application.Dto;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Enum;

namespace Pharmacy.Application.Features.Order.Queries.GetAll;

public class GetOrdersQuery : Pagination, IRequest<Result<PaginationResponse<GetOrdersResponse>>>
{
    public string? Name { get; set; }
    public string? PurchaseOrderNumber { get; set; }
    public OrderStatus? OrderStatus { get; set; }
    public DateTime? StartDate { get; set; } // Start date of the user to work in the system
    public DateTime? EndDate { get; set; }
}
