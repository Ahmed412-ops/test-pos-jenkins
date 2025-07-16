using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.APIs.Authorization;
using Pharmacy.Application.Constants.Permissions;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Application.Features.Medicine.Medicine.Commands.Create;
using Pharmacy.Application.Features.Medicine.Medicine.Commands.Delete;
using Pharmacy.Application.Features.Medicine.Medicine.Commands.Update;
using Pharmacy.Application.Features.Medicine.Medicine.Queries.DropDown;
using Pharmacy.Application.Features.Medicine.Medicine.Queries.GetAll;
using Pharmacy.Application.Features.Medicine.Medicine.Queries.GetById;
using Pharmacy.Application.Features.Medicine.Medicine.Queries.GetMedicineUnits;
using Pharmacy.Domain.Dto;

namespace Pharmacy.APIs.Controllers;

[Authorize]

public class MedicinesController(ISender sender) : BaseApiController
{
    private readonly ISender _mediator = sender;
    [HttpPost("Create")]
    [CheckPermission(PermissionConstant.Medicine.Add)]
    public async Task<ActionResult<Result<string>>> Create(CreateMedicineCommand command)   
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
    [HttpGet("GetAll")]
    [CheckPermission(PermissionConstant.Medicine.View)]
    public async Task<ActionResult<Result<PaginationResponse<GetMedicinesResponse>>>> GetAll([FromQuery] GetMedicinesQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
    [HttpGet("GetById")]
    [CheckPermission(PermissionConstant.Medicine.View)]
    public async Task<ActionResult<Result<GetMedicineResponse>>> GetById([FromQuery] GetMedicineQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
    [HttpPut("Update")]
    [CheckPermission(PermissionConstant.Medicine.Edit)]
    public async Task<ActionResult<Result<string>>> Update(UpdateMedicineCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
    [HttpGet("DropDown")]
    [CheckPermission(PermissionConstant.Medicine.View)]
    public async Task<ActionResult<Result<List<DropDownQueryResponse>>>> GetAllDropDown()
    {
        return BaseResponseHandler(await _mediator.Send(new MedicinesDropDownQuery()));
    }
    [HttpGet("GetMedicineUnits")]
    [CheckPermission(PermissionConstant.Medicine.View)]
    public async Task<ActionResult<Result<List<GetMedicineUnitsResponse>>>> GetMedicineUnits()
    {
        return BaseResponseHandler(await _mediator.Send(new GetMedicineUnitsQuery()));
    }
    [HttpDelete("Delete")]
    [CheckPermission(PermissionConstant.Medicine.Delete)]
    public async Task<ActionResult<Result<bool>>> Delete([FromQuery] DeleteMedicineCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
}
