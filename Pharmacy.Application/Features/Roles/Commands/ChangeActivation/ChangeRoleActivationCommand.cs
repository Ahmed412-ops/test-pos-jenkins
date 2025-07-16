using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Roles.Commands.ChangeActivation;

public class ChangeRoleActivationCommand : IRequest<Result<bool>>
{
    public Guid RoleId { get; set; }
  
}
