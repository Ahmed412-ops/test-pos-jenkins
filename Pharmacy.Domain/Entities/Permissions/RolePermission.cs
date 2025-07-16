using Pharmacy.Domain.Entities.Identity;

namespace Pharmacy.Domain.Entities.Permissions;

public class RolePermission : BaseEntity
{
    public Guid RoleId { get; set; }
    public virtual ApplicationRole? Role { get; set; }
    public Guid PermissionId { get; set; }
    public virtual Permission? Permission { get; set; }
}
