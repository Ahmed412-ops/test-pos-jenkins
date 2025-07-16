using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Food.Commands.Delete;

public class DeleteFoodCommand : IRequest<Result<bool>>
{
    public Guid Id { get; set; }
}
