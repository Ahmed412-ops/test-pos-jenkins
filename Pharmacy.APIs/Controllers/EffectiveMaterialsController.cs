using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.APIs.Authorization;
using Pharmacy.Application.Constants.Permissions;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Application.Features.EffectiveMaterial.EffectiveMaterial.Commands.Create;
using Pharmacy.Application.Features.EffectiveMaterial.EffectiveMaterial.Commands.Delete;
using Pharmacy.Application.Features.EffectiveMaterial.EffectiveMaterial.Commands.Update;
using Pharmacy.Application.Features.EffectiveMaterial.EffectiveMaterial.Queries.DropDown;
using Pharmacy.Application.Features.EffectiveMaterial.EffectiveMaterial.Queries.GetAll;
using Pharmacy.Application.Features.EffectiveMaterial.EffectiveMaterial.Queries.GetById;
using Pharmacy.Domain.Dto;

namespace Pharmacy.APIs.Controllers;

[Authorize]
public class EffectiveMaterialsController(ISender sender) : BaseApiController
{
    private readonly ISender _mediator = sender;
    [HttpPost("Create")]
    [CheckPermission(PermissionConstant.EffectiveMaterial.Add)]
    public async Task<ActionResult<Result<string>>> Create(CreateEffectiveMaterialCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
    [HttpGet("GetAll")]
    [CheckPermission(PermissionConstant.EffectiveMaterial.View)]
    public async Task<ActionResult<Result<PaginationResponse<GetEffectiveMaterialsResponse>>>> GetAll([FromQuery] GetEffectiveMaterialsQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
    [HttpGet("GetById")]
    [CheckPermission(PermissionConstant.EffectiveMaterial.View)]
    public async Task<ActionResult<Result<GetEffectiveMaterialResponse>>> GetById([FromQuery] GetEffectiveMaterialQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
    [HttpPut("Update")]
    [CheckPermission(PermissionConstant.EffectiveMaterial.Edit)]
    public async Task<ActionResult<Result<string>>> Update(UpdateEffectiveMaterialCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
    [HttpGet("DropDown")]
    [CheckPermission(PermissionConstant.EffectiveMaterial.View)]
    public async Task<ActionResult<Result<List<DropDownQueryResponse>>>> DropDown()
    {
        return BaseResponseHandler(await _mediator.Send(new EffectiveMaterialsDropDownQuery()));
    }
    [HttpDelete("Delete")]
    [CheckPermission(PermissionConstant.EffectiveMaterial.Delete)]
    public async Task<ActionResult<Result<bool>>> Delete(DeleteEffectiveMaterialCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
}
