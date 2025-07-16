using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Order.Commands.Update;

public class UpdateOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<UpdateOrderCommand, Result<string>>
    {
        private readonly IGenericRepository<Domain.Entities.Order.PurchaseOrder> _orderRepository = unitOfWork.GetRepository<Domain.Entities.Order.PurchaseOrder>();

        public override async Task<Result<string>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.FindAsync(
                s => s.Id == request.Id,
                Include: s => s.Include(s => s.Items));

            if (order == null)
                return Result<string>.Fail(Messages.NotFound);

            mapper.Map(request, order);

            await unitOfWork.SaveChangesAsync();

            return Result<string>.Success(Messages.SuccessfullyUpdated);
        }
    }

