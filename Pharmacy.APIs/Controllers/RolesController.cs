using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.APIs.Authorization;
using Pharmacy.Application.Constants.Permissions;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Application.Features.Roles.Commands.ChangeActivation;
using Pharmacy.Application.Features.Roles.Commands.Create;
using Pharmacy.Application.Features.Roles.Commands.Update;
using Pharmacy.Application.Features.Roles.Common;
using Pharmacy.Application.Features.Roles.Queries.GetAll;
using Pharmacy.Application.Features.Roles.Queries.GetById;
using Pharmacy.Application.Features.Roles.Queries.PermissionsDropDown;
using Pharmacy.Application.Features.Roles.Queries.RolesDropDown;
using Pharmacy.Domain.Dto;

namespace Pharmacy.APIs.Controllers;

[Authorize]
public class RolesController(ISender sender) : BaseApiController
{
  private readonly ISender _mediator = sender;
  [HttpPost("Create")]
  [CheckPermission(PermissionConstant.Role.Add)]
  public async Task<ActionResult<Result<string>>> Create(CreateRoleCommand command)
  {
    return BaseResponseHandler(await _mediator.Send(command));
  }
  [HttpGet("GetAll")]
  [CheckPermission(PermissionConstant.Role.View)]
  public async Task<ActionResult<Result<PaginationResponse<GetRoleResponse>>>> GetAll([FromQuery] GetRolesQuery query)
  {
    return BaseResponseHandler(await _mediator.Send(query));
  }
  [HttpGet("GetById")]
  [CheckPermission(PermissionConstant.Role.View)]
  public async Task<ActionResult<Result<GetRoleResponse>>> GetById([FromQuery] GetRoleQuery query)
  {
    return BaseResponseHandler(await _mediator.Send(query));
  }
  [HttpPut("Update")]
  [CheckPermission(PermissionConstant.Role.Edit)]
  public async Task<ActionResult<Result<string>>> Update(UpdateRoleCommand command)
  {
    return BaseResponseHandler(await _mediator.Send(command));
  }
  [HttpPut("ChangeActivation")]
  [CheckPermission(PermissionConstant.Role.Edit)]
  public async Task<ActionResult<Result<bool>>> ChangeActivation(ChangeRoleActivationCommand command)
  {
    return BaseResponseHandler(await _mediator.Send(command));
  }
  [HttpGet("RolesDropDown")]
  [CheckPermission(PermissionConstant.Role.View)]
  public async Task<ActionResult<Result<List<DropDownQueryResponse>>>> RolesDropDown()
  {
    return BaseResponseHandler(await _mediator.Send(new RolesDropDownQuery()));
  }
  [HttpGet("PermissionsDropDown")]
  [CheckPermission(PermissionConstant.Role.View)]
  public async Task<ActionResult<Result<List<PermissionsDropDownQueryResponse>>>> PermissionsDropDown()
  {
    return BaseResponseHandler(await _mediator.Send(new PermissionsDropDownQuery()));
  }

}
