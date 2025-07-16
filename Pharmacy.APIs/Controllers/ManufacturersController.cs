using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.APIs.Authorization;
using Pharmacy.Application.Constants.Permissions;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Application.Features.Manufacturer.Commands.Create;
using Pharmacy.Application.Features.Manufacturer.Commands.Delete;
using Pharmacy.Application.Features.Manufacturer.Commands.Update;
using Pharmacy.Application.Features.Manufacturer.Queries.DropDown;
using Pharmacy.Application.Features.Manufacturer.Queries.GetAll;
using Pharmacy.Application.Features.Manufacturer.Queries.GetById;
using Pharmacy.Domain.Dto;

namespace Pharmacy.APIs.Controllers;

[Authorize]

public class ManufacturersController(ISender sender) : BaseApiController
{
    private readonly ISender _mediator = sender;
    [HttpPost("Create")] 
    [CheckPermission(PermissionConstant.Manufacturer.View)]
    public async Task<ActionResult<Result<string>>> Create(CreateManufacturerCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
    [HttpGet("GetAll")]
    [CheckPermission(PermissionConstant.Manufacturer.View)]
    public async Task<ActionResult<Result<PaginationResponse<GetManufacturersResponse>>>> GetAll([FromQuery] GetManufacturersQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
    [HttpGet("GetById")]
    [CheckPermission(PermissionConstant.Manufacturer.View)]
    public async Task<ActionResult<Result<GetManufacturerResponse>>> GetById([FromQuery] GetManufacturerQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
    [HttpPut("Update")]
    [CheckPermission(PermissionConstant.Manufacturer.Edit)]
    public async Task<ActionResult<Result<string>>> Update(UpdateManufacturerCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }

    [HttpGet("DropDown")]
    [CheckPermission(PermissionConstant.Manufacturer.View)]
    public async Task<ActionResult<Result<List<DropDownQueryResponse>>>> DropDown()
    {
        return BaseResponseHandler(await _mediator.Send(new ManufacturersDropDownQuery()));
    }
    [HttpDelete("Delete")]
    [CheckPermission(PermissionConstant.Manufacturer.Delete)]
    public async Task<ActionResult<Result<bool>>> Delete([FromQuery] DeleteManufacturerCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }

}