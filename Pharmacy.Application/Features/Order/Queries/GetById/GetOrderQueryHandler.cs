using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Order.Queries.GetById;

public class GetOrderQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : BaseHandler<GetOrderQuery, Result<GetOrderResponse>>
{
    private readonly IGenericRepository<Domain.Entities.Order.PurchaseOrder> _orderRepo = unitOfWork.GetRepository<Domain.Entities.Order.PurchaseOrder>();


    public override async Task<Result<GetOrderResponse>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepo.FindAsync(
            o => o.Id == request.Id,
            Include: o => o.Include(i => i.Items)
                            .ThenInclude(m => m.MedicineUnit)
                            .ThenInclude(m => m.Medicine)
                            .Include(s => s.Supplier)
                            .Include(c=> c.Items)
                            .ThenInclude(m => m.MedicineUnit)
                            .ThenInclude(m => m.Unit),
            asNoTracking: true);

        if (order == null)
            return Result<GetOrderResponse>.Fail(Messages.NotFound);

        var response = mapper.Map<GetOrderResponse>(order);

        return Result<GetOrderResponse>.Success(response);
    }
}

