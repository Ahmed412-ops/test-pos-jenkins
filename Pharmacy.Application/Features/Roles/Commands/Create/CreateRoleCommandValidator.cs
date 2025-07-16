using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Entities.Identity;

namespace Pharmacy.Application.Features.Roles.Commands.Create;

public class CreateRoleCommandValidator: AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator(RoleManager<ApplicationRole> roleManager)
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(Messages.RoleNameIsRequired)
            .MustAsync(async (name, cancellation) =>
            {
                var role = await roleManager.FindByNameAsync(name);
                return role == null;
            }).WithMessage(Messages.RoleNameAlreadyExists);
    }
}
