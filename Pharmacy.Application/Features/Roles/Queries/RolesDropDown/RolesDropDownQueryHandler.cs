using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Domain;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Entities.Identity;

namespace Pharmacy.Application.Features.Roles.Queries.RolesDropDown;

public class RolesDropDownQueryHandler(
    RoleManager<ApplicationRole> rolesRepo,
    IMapper mapper)
        : BaseHandler<RolesDropDownQuery, Result<List<DropDownQueryResponse>>>
{
    private readonly RoleManager<ApplicationRole> _rolesRepo = rolesRepo;


    public override async Task<Result<List<DropDownQueryResponse>>> Handle(
        RolesDropDownQuery request,
        CancellationToken cancellationToken)
    {
        var roles = await _rolesRepo.Roles
            .Where(role => role.Name != nameof(UserRole.SuperAdmin))
            .ToListAsync(cancellationToken);
        var result = mapper.Map<List<DropDownQueryResponse>>(roles);
        return Result<List<DropDownQueryResponse>>.Success(result);
    }
}
