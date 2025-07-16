using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Order.Queries.DropDown;

public class OrdersDropDownQuery : IRequest<Result<List<OrdersDropDownQueryResponse>>>
{
    
}
