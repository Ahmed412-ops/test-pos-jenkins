using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Application.Services.Abstraction.GeneratorService;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Order.Commands.Create;

public class CreateOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IPurchaseOrderNumberGenerator purchaseOrderNumberGenerator) : BaseHandler<CreateOrderCommand, Result<string>>
{
    private readonly IGenericRepository<Domain.Entities.Order.PurchaseOrder> _orderRepository = unitOfWork.GetRepository<Domain.Entities.Order.PurchaseOrder>();

    private readonly IPurchaseOrderNumberGenerator _purchaseOrderNumberGenerator = purchaseOrderNumberGenerator;
    public override async Task<Result<string>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = mapper.Map<Domain.Entities.Order.PurchaseOrder>(request);
        order.PurchaseOrderNumber = _purchaseOrderNumberGenerator.GenerateUniquePurchaseOrderNumber(request.Name);
        await _orderRepository.AddAsync(order);
        await unitOfWork.SaveChangesAsync();
        return Result<string>.Success(Messages.SuccessfullyCreated);
    }
}
