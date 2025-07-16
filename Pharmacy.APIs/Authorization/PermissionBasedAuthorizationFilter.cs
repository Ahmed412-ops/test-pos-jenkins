using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Domain.Entities.Permissions;

namespace Pharmacy.APIs.Authorization;

public class PermissionBasedAuthorizationFilter(IUnitOfWork unitOfWork) : IAsyncAuthorizationFilter
{
    private readonly IGenericRepository<Permission> _permissionRepository =
        unitOfWork.GetRepository<Permission>();

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var attribute = context
            .ActionDescriptor.EndpointMetadata.OfType<CheckPermissionAttribute>()
            .FirstOrDefault();

        if (attribute == null)
            return;

        var claimIdentity = context.HttpContext.User.Identity as ClaimsIdentity;
        if (claimIdentity == null || !claimIdentity.IsAuthenticated)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var userId = claimIdentity.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        var hasPermission = await _permissionRepository.IsExistsAsync(x =>
            x.Title == attribute.Permission
            && x.RolePermissions.Any(rp =>
                rp.Role!.UserRoles.Any(ur => ur.UserId.ToString() == userId)
            )
        );

        if (!hasPermission)
            context.Result = new ForbidResult();
        return;
    }
}
