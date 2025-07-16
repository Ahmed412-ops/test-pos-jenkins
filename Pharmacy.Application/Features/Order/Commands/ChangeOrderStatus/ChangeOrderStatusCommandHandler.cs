using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Order.Commands.ChangeOrderStatus;

public class ChangeOrderStatusCommandHandler(IUnitOfWork unitOfWork)
    : BaseHandler<ChangeOrderStatusCommand, Result<bool>>
{
    private readonly IGenericRepository<Domain.Entities.Order.PurchaseOrder> _orderRepository = unitOfWork.GetRepository<Domain.Entities.Order.PurchaseOrder>();
    public override async Task<Result<bool>> Handle(ChangeOrderStatusCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.FindAsync(f => f.Id == request.Id);
        if (order == null)
            return Result<bool>.Fail(Messages.NotFound);

        order.OrderStatus = request.OrderStatus;
        await unitOfWork.SaveChangesAsync();

        return Result<bool>.Success(true, Messages.SuccessfullyUpdated);
    }
}
