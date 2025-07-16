using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Entities.Identity;

namespace Pharmacy.Application.Features.AccountsManagement.Command.Update;

public class UpdateAccountCommandValidator :  AbstractValidator<UpdateAccountCommand>
{
    public UpdateAccountCommandValidator(
        UserManager<ApplicationUser>  userManager
    )  
    {
      RuleFor(x => x.FullName)
        .NotEmpty().WithMessage(Messages.FullNameIsRequired);

      RuleFor(p => p.UserName)
        .NotEmpty().WithMessage(Messages.UserNameIsRequired)
        .MaximumLength(20).WithMessage(Messages.UserNameIsTooLong)
        .MustAsync(async (userName, cancellation) =>
        {
            var user = await userManager.FindByNameAsync(userName);
            return user == null;
        }).WithMessage(Messages.UserNameAlreadyExists);

      RuleFor(p => p.Email)
        .EmailAddress().WithMessage(Messages.InvalidEmail)
        .MaximumLength(40).WithMessage(Messages.EmailIsTooLong)
        .MustAsync(async (email, cancellation) =>
        {
            return await userManager.FindByEmailAsync(email) == null;
        }).WithMessage(Messages.EmailAlreadyExists);

      RuleFor(p => p.PhoneNumber)
        .MaximumLength(20).WithMessage(Messages.PhoneNumberIsTooLong);

      RuleFor(p => p.DateOfBirth)
        .LessThan(DateTime.Now).WithMessage(Messages.InvalidDateOfBirth);
    }
}
