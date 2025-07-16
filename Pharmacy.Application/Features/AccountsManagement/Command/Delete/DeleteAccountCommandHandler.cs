using Microsoft.AspNetCore.Identity;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Entities.Identity;

namespace Pharmacy.Application.Features.AccountsManagement.Command.Delete;

public class DeleteAccountCommandHandler(UserManager<ApplicationUser> userManager) : BaseHandler<DeleteAccountCommand, Result<bool>>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    public override async Task<Result<bool>> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user == null)
            return Result<bool>.Fail(Messages.SomethingWentWrong);

        user.Is_Deleted = true;
        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
            return Result<bool>.Fail(Messages.SomethingWentWrong);

        return Result<bool>.Success();
    }
}
