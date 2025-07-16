using MediatR;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Features.Roles.Common;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Roles.Queries.GetAll;

public class GetRolesQuery : Pagination, IRequest<Result<PaginationResponse<GetRoleResponse>>>
{
  public string? Name { get; set; }  
}
