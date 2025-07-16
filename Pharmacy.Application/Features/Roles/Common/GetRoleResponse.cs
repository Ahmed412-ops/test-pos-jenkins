namespace Pharmacy.Application.Features.Roles.Common;

public class GetRoleResponse
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public bool IsActive { get; set; }
    public List<PermissionsDropDownQueryResponse> RolePermissions { get; set; } = [];    
}

public class PermissionsDto
{
    public Guid Id { get; set; }
    public required string Title { get; set; }    
}
