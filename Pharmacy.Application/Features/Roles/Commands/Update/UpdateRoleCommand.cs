using MediatR;
using Pharmacy.Application.Features.Roles.Commands.Create;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Roles.Commands.Update;

public class UpdateRoleCommand : CreateRoleCommand, IRequest<Result<string>>
{
    public Guid Id { get; set; }
}
