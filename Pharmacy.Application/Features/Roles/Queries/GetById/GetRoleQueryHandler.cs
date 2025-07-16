using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Features.Roles.Common;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Entities.Identity;

namespace Pharmacy.Application.Features.Roles.Queries.GetById;

public class GetRoleQueryHandler(RoleManager<ApplicationRole> roleManager, IMapper mapper) : BaseHandler<GetRoleQuery, Result<GetRoleResponse>>
{
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;


    public override async Task<Result<GetRoleResponse>> Handle(GetRoleQuery request, CancellationToken cancellationToken)
    {
        var role = await _roleManager.Roles
            .Include(r => r.RolePermissions)
            .ThenInclude(rp => rp.Permission)
            .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

        if (role == null)
            return Result<GetRoleResponse>.Fail(Messages.RoleNotFound);

        var response = mapper.Map<GetRoleResponse>(role);

        return Result<GetRoleResponse>.Success(response);
    }
}
