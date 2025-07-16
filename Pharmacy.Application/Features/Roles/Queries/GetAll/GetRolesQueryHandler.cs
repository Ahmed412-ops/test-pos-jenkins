using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Features.Roles.Common;
using Pharmacy.Application.Helper.Extensions;
using Pharmacy.Domain;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Entities.Identity;

namespace Pharmacy.Application.Features.Roles.Queries.GetAll;

public class GetRolesQueryHandler(RoleManager<ApplicationRole> roleManager, IMapper mapper)
    : BaseHandler<GetRolesQuery, Result<PaginationResponse<GetRoleResponse>>>
{
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;


    public override async Task<Result<PaginationResponse<GetRoleResponse>>> Handle(
        GetRolesQuery request,
        CancellationToken cancellationToken
    )
    {
        var rolesQuery = _roleManager
            .Roles.Include(r => r.RolePermissions)
            .ThenInclude(rp => rp.Permission)
            .Where(r => r.Name != nameof(UserRole.SuperAdmin))
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Name))
            rolesQuery = rolesQuery.Where(a =>
                (a.Name != null && a.Name.Contains(request.Name))
                || (a.Name != null && a.Name.Contains(request.Name))
            );

        var count = await rolesQuery.CountAsync(cancellationToken);

        var response = rolesQuery
            .Select(a => mapper.Map<GetRoleResponse>(a))
            .Paginate(request)
            .ToList();

        return Result<PaginationResponse<GetRoleResponse>>.Success(
            new PaginationResponse<GetRoleResponse> { Data = response, Count = count }
        );
    }
}
