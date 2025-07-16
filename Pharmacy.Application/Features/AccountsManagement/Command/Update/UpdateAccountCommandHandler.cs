using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Entities.Identity;

namespace Pharmacy.Application.Features.AccountsManagement.Command.Update;

public class UpdateAccountCommandHandler(
    UserManager<ApplicationUser> userManager,
    IMapper mapper
) : BaseHandler<UpdateAccountCommand, Result<string>>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    public override async Task<Result<string>> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id.ToString());
        if (user == null)
            return Result<string>.Fail(Messages.UserNotFound);

        mapper.Map(request, user);
        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
            return Result<string>.Fail(Messages.SomethingWentWrong);

        return Result<string>.Success(Messages.AccountUpdatedSuccessfully);

    }


}
