using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Order.Queries.DropDown;

public class OrdersDropDownQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<OrdersDropDownQuery, Result<List<OrdersDropDownQueryResponse>>>
{
    private readonly IGenericRepository<Domain.Entities.Order.PurchaseOrder> _orderRepo = unitOfWork.GetRepository<Domain.Entities.Order.PurchaseOrder>();

    public override async Task<Result<List<OrdersDropDownQueryResponse>>> Handle(
        OrdersDropDownQuery request,
        CancellationToken cancellationToken)
    {
        var orders = await _orderRepo.GetAllAsync(d => !d.Is_Deleted);
        var result = mapper.Map<List<OrdersDropDownQueryResponse>>(orders);
        return Result<List<OrdersDropDownQueryResponse>>.Success(result);
    }
}
