using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.APIs.Authorization;
using Pharmacy.Application.Constants.Permissions;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Application.Features.SideEffect.Commands.Create;
using Pharmacy.Application.Features.SideEffect.Commands.Delete;
using Pharmacy.Application.Features.SideEffect.Commands.Update;
using Pharmacy.Application.Features.SideEffect.Queries.DropDown;
using Pharmacy.Application.Features.SideEffect.Queries.GetAll;
using Pharmacy.Application.Features.SideEffect.Queries.GetById;
using Pharmacy.Domain.Dto;

namespace Pharmacy.APIs.Controllers;

[Authorize]

public class SideEffectsController(ISender sender) : BaseApiController
{
    private readonly ISender _mediator = sender;
    [HttpPost("Create")] 
    [CheckPermission(PermissionConstant.SideEffect.Add)]
    public async Task<ActionResult<Result<string>>> Create(CreateSideEffectCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
    [HttpGet("GetAll")]
    [CheckPermission(PermissionConstant.SideEffect.View)]
    public async Task<ActionResult<Result<PaginationResponse<GetSideEffectsResponse>>>> GetAll([FromQuery] GetSideEffectsQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
    [HttpGet("GetById")]
    [CheckPermission(PermissionConstant.SideEffect.View)]
    public async Task<ActionResult<Result<GetSideEffectResponse>>> GetById([FromQuery] GetSideEffectQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
    [
    HttpPut("Update")]
    [CheckPermission(PermissionConstant.SideEffect.Edit)]
    public async Task<ActionResult<Result<string>>> Update(UpdateSideEffectCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }

    [HttpGet("DropDown")]
    [CheckPermission(PermissionConstant.SideEffect.View)]
    public async Task<ActionResult<Result<List<DropDownQueryResponse>>>> DropDown()
    {
        return BaseResponseHandler(await _mediator.Send(new SideEffectsDropDownQuery()));
    }
    [HttpDelete("Delete")]
    [CheckPermission(PermissionConstant.SideEffect.Delete)]
    public async Task<ActionResult<Result<bool>>> Delete([FromQuery] DeleteSideEffectCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }

}