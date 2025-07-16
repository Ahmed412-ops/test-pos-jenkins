using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Unit.Commands.Delete;

public class DeleteUnitCommand : IRequest<Result<bool>>
{
    public Guid Id { get; set; }
}
