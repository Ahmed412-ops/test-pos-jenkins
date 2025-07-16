using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Food.Queries.GetById;

public class GetFoodByIdQuery : IRequest<Result<GetFoodByIdResponse>>
{
    public Guid Id { get; set; }
}
