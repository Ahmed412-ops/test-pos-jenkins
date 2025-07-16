using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.APIs.Authorization;
using Pharmacy.Application.Constants.Permissions;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Application.Features.Uses.Commands.Create;
using Pharmacy.Application.Features.Uses.Commands.Delete;
using Pharmacy.Application.Features.Uses.Commands.Update;
using Pharmacy.Application.Features.Uses.Queries.DropDown;
using Pharmacy.Application.Features.Uses.Queries.GetAll;
using Pharmacy.Application.Features.Uses.Queries.GetById;
using Pharmacy.Domain.Dto;

namespace Pharmacy.APIs.Controllers;

[Authorize]

public class UsesController(ISender sender) : BaseApiController
{
    private readonly ISender _mediator = sender;
    [HttpPost("Create")]
    [CheckPermission(PermissionConstant.Use.Add)]
    public async Task<ActionResult<Result<string>>> Create(CreateUseCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
    [HttpGet("GetAll")]
    [CheckPermission(PermissionConstant.Use.View)]
    public async Task<ActionResult<Result<PaginationResponse<GetUsesResponse>>>> GetAll([FromQuery] GetUsesQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
    [HttpGet("GetById")]
    [CheckPermission(PermissionConstant.Use.View)]
    public async Task<ActionResult<Result<GetUseResponse>>> GetById([FromQuery] GetUseQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
    [HttpPut("Update")]
    [CheckPermission(PermissionConstant.Use.Edit)]
    public async Task<ActionResult<Result<string>>> Update(UpdateUseCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
    [HttpGet("DropDown")]
    [CheckPermission(PermissionConstant.Use.View)]
    public async Task<ActionResult<Result<List<DropDownQueryResponse>>>> DropDown()
    {
        return BaseResponseHandler(await _mediator.Send(new UsesDropDownQuery()));
    }
    [HttpDelete("Delete")]
    [CheckPermission(PermissionConstant.Use.Delete)]
    public async Task<ActionResult<Result<bool>>> Delete(DeleteUsesCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
}
