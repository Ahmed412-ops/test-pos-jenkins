using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.APIs.Authorization;
using Pharmacy.Application.Constants.Permissions;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Application.Features.Symptom.Commands.Create;
using Pharmacy.Application.Features.Symptom.Commands.Delete;
using Pharmacy.Application.Features.Symptom.Commands.Update;
using Pharmacy.Application.Features.Symptom.Queries.DropDown;
using Pharmacy.Application.Features.Symptom.Queries.GetAll;
using Pharmacy.Application.Features.Symptom.Queries.GetById;
using Pharmacy.Domain.Dto;

namespace Pharmacy.APIs.Controllers;

[Authorize]

public class SymptomsController(ISender sender) : BaseApiController
{
    private readonly ISender _mediator = sender;
    [HttpPost("Create")]
    [CheckPermission(PermissionConstant.Symptom.Add)]
    public async Task<ActionResult<Result<string>>> Create(CreateSymptomCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
    [HttpGet("GetAll")]
    [CheckPermission(PermissionConstant.Symptom.View)]
    public async Task<ActionResult<Result<PaginationResponse<GetSymptomsResponse>>>> GetAll([FromQuery] GetSymptomsQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
    [HttpGet("GetById")]
    [CheckPermission(PermissionConstant.Symptom.View)]
    public async Task<ActionResult<Result<GetSymptomResponse>>> GetById([FromQuery]GetSymptomQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
    [HttpPut("Update")]
    [CheckPermission(PermissionConstant.Symptom.Edit)]
    public async Task<ActionResult<Result<string>>> Update(UpdateSymptomCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
    [HttpGet("DropDown")]
    [CheckPermission(PermissionConstant.Symptom.View)]
    public async Task<ActionResult<Result<List<DropDownQueryResponse>>>> GetDropDown()
    {
        return BaseResponseHandler(await _mediator.Send(new SymptomsDropDownQuery()));
    }
    [HttpDelete("Delete")]
    [CheckPermission(PermissionConstant.Symptom.Delete)]
    public async Task<ActionResult<Result<bool>>> Delete(DeleteSymptomCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
}