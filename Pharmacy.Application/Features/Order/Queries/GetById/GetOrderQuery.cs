using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Order.Queries.GetById;

public class GetOrderQuery : IRequest<Result<GetOrderResponse>>
{
    public Guid Id { get; set; } 
}

