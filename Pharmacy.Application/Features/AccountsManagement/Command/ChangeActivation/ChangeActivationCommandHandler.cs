using Microsoft.AspNetCore.Identity;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Entities.Identity;

namespace Pharmacy.Application.Features.AccountsManagement.Command.ChangeActivation;

public class ChangeActivationCommandHandler(UserManager<ApplicationUser> userManager) : BaseHandler<ChangeActivationCommand, Result<bool>>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    public override async Task<Result<bool>> Handle(ChangeActivationCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user == null)
            return Result<bool>.Fail(Messages.SomethingWentWrong);

        user.Is_Active = !user.Is_Active;
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
            return Result<bool>.Fail(Messages.SomethingWentWrong);

        return Result<bool>.Success();
    }
}

