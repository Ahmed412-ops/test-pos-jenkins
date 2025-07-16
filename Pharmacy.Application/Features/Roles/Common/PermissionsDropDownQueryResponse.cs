namespace Pharmacy.Application.Features.Roles.Common;

public class PermissionsDropDownQueryResponse
{
  public required string FeatureTitle { get; set; }  
  public List<PermissionDto> Permissions { get; set; } =[];
}

public class PermissionDto
{
  public Guid Id { get; set; }
  public required string Title { get; set; }  
}