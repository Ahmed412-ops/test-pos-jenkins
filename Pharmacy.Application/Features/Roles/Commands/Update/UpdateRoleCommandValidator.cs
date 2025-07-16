using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Entities.Identity;

namespace Pharmacy.Application.Features.Roles.Commands.Update;

public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
{
    public UpdateRoleCommandValidator(RoleManager<ApplicationRole> roleManager)
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x)
            .MustAsync(async (command, cancellation) =>
            {
                var role = await roleManager.FindByNameAsync(command.Name);
                return role == null || role.Id == command.Id;
            }).WithMessage(Messages.RoleNameAlreadyExists);

    }

}
