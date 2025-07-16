using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Roles.Commands.Create;

public class CreateRoleCommand : IRequest<Result<string>>
{
    public required string Name { get; set; }
    public List<Guid> Permissions { get; set; } = [];
}
