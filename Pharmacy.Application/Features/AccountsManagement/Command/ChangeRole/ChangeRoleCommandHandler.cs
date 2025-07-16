using Microsoft.AspNetCore.Identity;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Entities.Identity;

namespace Pharmacy.Application.Features.AccountsManagement.Command.ChangeRole;

public class ChangeRoleCommandHandler(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager) : BaseHandler<ChangeRoleCommand, Result<bool>>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
    public override async Task<Result<bool>> Handle(ChangeRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user == null)
            return Result<bool>.Fail(Messages.UserNotFound);

        var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
        if (role == null)
            return Result<bool>.Fail(Messages.RoleNotFound);

        var userRoles = await _userManager.GetRolesAsync(user);
        var result = await _userManager.RemoveFromRolesAsync(user, userRoles);
        if (!result.Succeeded)
            return Result<bool>.Fail(Messages.SomethingWentWrong);

        result = await _userManager.AddToRoleAsync(user, role.Name!);
        if (!result.Succeeded)
            return Result<bool>.Fail(Messages.SomethingWentWrong);

        return Result<bool>.Success();
    }
}

