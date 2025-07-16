using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.SideEffect.Commands.Delete;

public class DeleteSideEffectCommand : IRequest<Result<bool>>
{
    public Guid Id { get; set; }
}
