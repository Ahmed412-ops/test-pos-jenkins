namespace Pharmacy.Domain.Entities.Permissions;

public class Permission  : BaseEntity
{
    public required string Title { get; set; }
    public ICollection<RolePermission> RolePermissions { get; set; } = [];

}
