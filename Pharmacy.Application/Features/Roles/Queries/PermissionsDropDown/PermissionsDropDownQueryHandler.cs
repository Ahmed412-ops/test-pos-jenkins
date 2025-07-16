using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Features.Roles.Common;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Entities.Permissions;

namespace Pharmacy.Application.Features.Roles.Queries.PermissionsDropDown;

public class PermissionsDropDownQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : BaseHandler<PermissionsDropDownQuery, Result<List<PermissionsDropDownQueryResponse>>>
{
    private readonly IGenericRepository<Permission> _permissionRepo = unitOfWork.GetRepository<Permission>();

    public override async Task<Result<List<PermissionsDropDownQueryResponse>>> Handle(
        PermissionsDropDownQuery request,
        CancellationToken cancellationToken)
    {
        // Fetch all permissions from the repository
        var permissions = await _permissionRepo.GetAllAsync(p => !p.Is_Deleted);

        // Group permissions by feature name (the part before the dot)
        var groupedPermissions = mapper.Map<List<PermissionsDropDownQueryResponse>>(permissions);

        return Result<List<PermissionsDropDownQueryResponse>>.Success(groupedPermissions);
    }
  
}
