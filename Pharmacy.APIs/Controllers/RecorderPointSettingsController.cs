using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.APIs.Authorization;
using Pharmacy.Application.Constants.Permissions;
using Pharmacy.Application.Features.Stock.RecorderSettings.Commands.Create;
using Pharmacy.Application.Features.Stock.RecorderSettings.Commands.Update;
using Pharmacy.Application.Features.Stock.RecorderSettings.Queries.GetById;
using Pharmacy.Domain.Dto;

namespace Pharmacy.APIs.Controllers;

[Authorize]
public class RecorderPointSettingsController(ISender sender) : BaseApiController
{
    private readonly ISender _mediator = sender;
    [HttpPost("Create")]
    [CheckPermission(PermissionConstant.RecorderPoint.Add)]
    public async Task<ActionResult<Result<string>>> Create(CreateRecorderPointCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
    [HttpGet("GetById")]
    [CheckPermission(PermissionConstant.RecorderPoint.View)]
    public async Task<ActionResult<Result<GetRecorderPointResponse>>> GetById([FromQuery] GetRecorderPointQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
    [HttpPut("Update")]
    [CheckPermission(PermissionConstant.RecorderPoint.Edit)]
    public async Task<ActionResult<Result<string>>> Update(UpdateRecorderPointCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
}
