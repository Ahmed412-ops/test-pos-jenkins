using Microsoft.AspNetCore.Identity;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Application.Services.Abstraction.EmailService;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Entities.Identity;

namespace Pharmacy.Application.Features.Auth.Command.ResetPassword;

public class ResetPasswordCommandHandler(UserManager<ApplicationUser> userManager, IEmailService emailService)
    : BaseHandler<ResetPasswordCommand, Result<string>>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IEmailService _emailService = emailService;

    public override async Task<Result<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);

        if (user == null || user.Is_Deleted)
            return Result<string>.Fail(Messages.UserNotFound);

        if(!user.Is_Active)
            return Result<string>.Fail(Messages.YourAccountIsDeactivated);

        var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, request.OldPassword);
        if (!isPasswordCorrect)
            return Result<string>.Fail(Messages.IncorrectPassword);
        
        if(request.OldPassword == request.NewPassword)
            return Result<string>.Fail(Messages.NewPasswordMustBeDifferent);

        var passwordHasher = new PasswordHasher<ApplicationUser>();
        user.PasswordHash = passwordHasher.HashPassword(user, request.NewPassword);

        // Update the user in the database
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
            return Result<string>.Fail(result.Errors.Select(x => x.Description).ToList());

        await _emailService.SendEmailAsync(user.Full_Name, user.Email!, user.UserName!, request.NewPassword);
        
        return Result<string>.Success(Messages.PasswordResetSuccessfully);
    }
}

