using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.AccountsManagement.Command.ChangeRole;

public class ChangeRoleCommand : IRequest<Result<bool>>
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}

