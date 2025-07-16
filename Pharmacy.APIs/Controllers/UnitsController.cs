using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.APIs.Authorization;
using Pharmacy.Application.Constants.Permissions;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Application.Features.Unit.Commands.Create;
using Pharmacy.Application.Features.Unit.Commands.Delete;
using Pharmacy.Application.Features.Unit.Commands.Update;
using Pharmacy.Application.Features.Unit.Queries.DropDown;
using Pharmacy.Application.Features.Unit.Queries.GetAll;
using Pharmacy.Application.Features.Unit.Queries.GetById;
using Pharmacy.Domain.Dto;

namespace Pharmacy.APIs.Controllers;

[Authorize]

public class UnitsController(ISender sender) : BaseApiController
{
    private readonly ISender _mediator = sender;
    [HttpPost("Create")]
    [CheckPermission(PermissionConstant.Unit.Add)]
    public async Task<ActionResult<Result<string>>> Create(CreateUnitCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
    [HttpGet("GetAll")]
    [CheckPermission(PermissionConstant.Unit.View)]
    public async Task<ActionResult<Result<PaginationResponse<GetUnitsResponse>>>> GetAll([FromQuery] GetUnitsQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
    [HttpGet("GetById")]
    [CheckPermission(PermissionConstant.Unit.View)]
    public async Task<ActionResult<Result<GetUnitResponse>>> GetById([FromQuery] GetUnitQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
    [HttpPut("Update")]
    [CheckPermission(PermissionConstant.Unit.Edit)]
    public async Task<ActionResult<Result<string>>> Update(UpdateUnitCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
    [HttpGet("DropDown")]
    [CheckPermission(PermissionConstant.Unit.View)]
    public async Task<ActionResult<Result<List<DropDownQueryResponse>>>> DropDown()
    {
        return BaseResponseHandler(await _mediator.Send(new UnitsDropDownQuery()));
    }
    [HttpDelete("Delete")]
    [CheckPermission(PermissionConstant.Unit.Delete)]
    public async Task<ActionResult<Result<bool>>> Delete(DeleteUnitCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
}
