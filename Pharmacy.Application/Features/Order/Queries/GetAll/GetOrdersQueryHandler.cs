using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Helper.Extensions;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Enum;

namespace Pharmacy.Application.Features.Order.Queries.GetAll;

public class GetOrdersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<GetOrdersQuery, Result<PaginationResponse<GetOrdersResponse>>>
{
    private readonly IGenericRepository<Domain.Entities.Order.PurchaseOrder> _orderRepo = unitOfWork.GetRepository<Domain.Entities.Order.PurchaseOrder>();

    public override async Task<Result<PaginationResponse<GetOrdersResponse>>> Handle(
        GetOrdersQuery request,
        CancellationToken cancellationToken
    )
    {
        var query = await _orderRepo.GetAllQueryableAsync(d=>!d.Is_Deleted);
        if(!string.IsNullOrWhiteSpace(request.Name))
            query = query.Where(a => a.Name.Contains(request.Name));

        if(!string.IsNullOrWhiteSpace(request.PurchaseOrderNumber))
            query = query.Where(a => a.PurchaseOrderNumber.Contains(request.PurchaseOrderNumber));

        query = request.OrderStatus switch
        {
            OrderStatus.Pending => query.Where(a => a.OrderStatus == OrderStatus.Pending),
            OrderStatus.Delivered => query.Where(a => a.OrderStatus == OrderStatus.Delivered),
            OrderStatus.Rejected => query.Where(a => a.OrderStatus == OrderStatus.Rejected),
            _ => query
        };

        if (request.StartDate.HasValue)
            query = query.Where(a => a.Created_At >= request.StartDate);

        if (request.EndDate.HasValue)
            query = query.Where(a => a.Created_At <= request.EndDate);

        var count = await query.CountAsync(cancellationToken);

        var response = query
            .Select(a => mapper.Map<GetOrdersResponse>(a))
            .Paginate(request)
            .ToList();

        return Result<PaginationResponse<GetOrdersResponse>>.Success(
            new PaginationResponse<GetOrdersResponse> { Data = response, Count = count }
        );
    }
}
