using Microsoft.AspNetCore.Identity;
using Pharmacy.Domain.Entities.Permissions;

namespace Pharmacy.Domain.Entities.Identity;

public class ApplicationRole : IdentityRole<Guid>
{
    public bool Is_Active { get; set; } = true;
    public virtual ICollection<ApplicationUserRole> UserRoles { get; set; } = [];
    public virtual ICollection<RolePermission> RolePermissions { get; set; } = [];
}

