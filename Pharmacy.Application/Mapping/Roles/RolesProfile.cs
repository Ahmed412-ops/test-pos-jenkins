using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Application.Features.Roles.Commands.Create;
using Pharmacy.Application.Features.Roles.Commands.Update;
using Pharmacy.Application.Features.Roles.Common;
using Pharmacy.Domain.Entities.Identity;
using Pharmacy.Domain.Entities.Permissions;

namespace Pharmacy.Application.Mapping.Roles;

public class RolesProfile : MappingProfileBase
{
    public RolesProfile()
    {
        CreateMap<ApplicationRole, DropDownQueryResponse>();

        CreateMap<CreateRoleCommand, ApplicationRole>()
            .ForMember(
                d => d.RolePermissions,
                opt =>
                    opt.MapFrom(
                        (src, dest) =>
                            src.Permissions.Select(permissionId => new RolePermission
                            {
                                PermissionId = permissionId,
                                RoleId = dest.Id, // Assign RoleId instead of creating a new Role object
                            })
                    )
            );

        CreateMap<UpdateRoleCommand, ApplicationRole>()
            .IncludeBase<CreateRoleCommand, ApplicationRole>();

        // Mapping RolePermission for potential uses
        CreateMap<RolePermission, RolePermission>()
            .ForMember(d => d.Permission, opt => opt.Ignore()) // Avoid creating new Permission
            .ForMember(d => d.Role, opt => opt.Ignore());

        CreateMap<ApplicationRole, GetRoleResponse>()
            .ForMember(d => d.RolePermissions, opt => opt.MapFrom(src => src.RolePermissions.Select(rp => rp.Permission).ToList()));

        CreateMap<List<Permission>, List<PermissionsDropDownQueryResponse>>()
            .ConvertUsing((src, dest, context) =>
            {
                var permissions = src;
                var groupedPermissions = permissions
                    .GroupBy(p => p.Title.Split('.')[0]) // Extract the feature name before the dot
                    .Select(g => new PermissionsDropDownQueryResponse
                    {
                        FeatureTitle = g.Key, // Set the feature name (e.g., "AccountManagement")
                        Permissions = g.Select(p => new PermissionDto
                        {
                            Id = p.Id,
                            Title = p.Title.Split('.')[1]
                        }).ToList()
                    })
                    .ToList();

                return groupedPermissions;
            });
        
    }
}
