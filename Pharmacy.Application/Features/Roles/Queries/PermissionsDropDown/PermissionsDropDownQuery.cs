using MediatR;
using Pharmacy.Application.Features.Roles.Common;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Roles.Queries.PermissionsDropDown;

public class PermissionsDropDownQuery : IRequest<Result<List<PermissionsDropDownQueryResponse>>>
{
    
}
