using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Entities.Identity;

namespace Pharmacy.Application.Features.Roles.Commands.Update;

public class UpdateRoleCommandHandler(RoleManager<ApplicationRole> roleManager, IMapper mapper)
    : BaseHandler<UpdateRoleCommand, Result<string>>
{
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;

    public override async Task<Result<string>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleManager.Roles
            .Include(r => r.RolePermissions)
            .ThenInclude(rp => rp.Permission)
            .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

        if (role == null)
            return Result<string>.Fail(Messages.RoleNotFound);

        role.RolePermissions.Clear();

        mapper.Map(request, role);

        var result = await _roleManager.UpdateAsync(role);
        if (!result.Succeeded)
            return Result<string>.Fail(Messages.SomethingWentWrong);

        return Result<string>.Success(Messages.RoleUpdatedSuccessfully);
    }
}
