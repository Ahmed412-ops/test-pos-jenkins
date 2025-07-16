using Microsoft.AspNetCore.Identity;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Entities.Identity;

namespace Pharmacy.Application.Features.Roles.Commands.ChangeActivation;

public class ChangeRoleActivationCommandHandler(RoleManager<ApplicationRole> roleManager) : BaseHandler<ChangeRoleActivationCommand, Result<bool>>
{
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
    public override async Task<Result<bool>> Handle(ChangeRoleActivationCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
        if (role == null)
            return Result<bool>.Fail(Messages.SomethingWentWrong);

        role.Is_Active = !role.Is_Active;
        var result = await _roleManager.UpdateAsync(role);
        if (!result.Succeeded)
            return Result<bool>.Fail(Messages.SomethingWentWrong);

        return Result<bool>.Success();
    }
}
