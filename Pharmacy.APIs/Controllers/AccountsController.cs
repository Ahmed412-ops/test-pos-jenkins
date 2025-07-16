using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.APIs.Authorization;
using Pharmacy.Application.Constants.Permissions;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Features.AccountsManagement.Command.ChangeActivation;
using Pharmacy.Application.Features.AccountsManagement.Command.ChangeRole;
using Pharmacy.Application.Features.AccountsManagement.Command.Create;
using Pharmacy.Application.Features.AccountsManagement.Command.Delete;
using Pharmacy.Application.Features.AccountsManagement.Command.Update;
using Pharmacy.Application.Features.AccountsManagement.Queries;
using Pharmacy.Application.Features.AccountsManagement.Queries.GetById;
using Pharmacy.Domain.Dto;

namespace Pharmacy.APIs.Controllers;
[Authorize]
public class AccountsController(ISender sender) : BaseApiController
{
  private readonly ISender _mediator = sender;
  [HttpPost("Create")]
  public async Task<ActionResult<Result<string>>> CreateAccount([FromBody] CreateAccountCommand command)
  {
    return BaseResponseHandler(await _mediator.Send(command));
  }
  [CheckPermission(PermissionConstant.AccountManagement.View)]
  [HttpGet("GetAll")]
  public async Task<ActionResult<Result<PaginationResponse<GetAccountsResponse>>>> GetAccounts([FromQuery] GetAccountsQuery query)
  {
    return BaseResponseHandler(await _mediator.Send(query));
  }
  [CheckPermission(PermissionConstant.AccountManagement.View)]
  [HttpGet("GetById")]
  public async Task<ActionResult<Result<GetAccountResponse>>> GetAccount([FromQuery] GetAccountQuery query)
  {
    return BaseResponseHandler(await _mediator.Send(query));
  }
  [CheckPermission(PermissionConstant.AccountManagement.Edit)]
  [HttpPut("Update")]
  public async Task<ActionResult<Result<string>>> UpdateAccount([FromBody] UpdateAccountCommand command)
  {
    return BaseResponseHandler(await _mediator.Send(command));
  }
  [CheckPermission(PermissionConstant.AccountManagement.Edit)]
  [HttpPut("ChangeActivation")]
  public async Task<ActionResult<Result<bool>>> ChangeActivation([FromBody] ChangeActivationCommand command)
  {
    return BaseResponseHandler(await _mediator.Send(command));
  }
  [CheckPermission(PermissionConstant.AccountManagement.Edit)]
  [HttpPut("ChangeRole")]
  public async Task<ActionResult<Result<bool>>> ChangeRole([FromBody] ChangeRoleCommand command)
  {
    return BaseResponseHandler(await _mediator.Send(command));
  }
  [CheckPermission(PermissionConstant.AccountManagement.Delete)]
  [HttpDelete("Delete")]
  public async Task<ActionResult<Result<bool>>> DeleteAccount([FromQuery] DeleteAccountCommand command)
  {
    return BaseResponseHandler(await _mediator.Send(command));
  }

}
