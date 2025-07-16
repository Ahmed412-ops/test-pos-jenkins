using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.APIs.Authorization;
using Pharmacy.Application.Constants.Permissions;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Application.Features.DosageForm.Commands.Create;
using Pharmacy.Application.Features.DosageForm.Commands.Delete;
using Pharmacy.Application.Features.DosageForm.Commands.Update;
using Pharmacy.Application.Features.DosageForm.Queries.DropDown;
using Pharmacy.Application.Features.DosageForm.Queries.GetAll;
using Pharmacy.Application.Features.DosageForm.Queries.GetById;
using Pharmacy.Domain.Dto;

namespace Pharmacy.APIs.Controllers;

[Authorize]

public class DosageFormsController(ISender sender) : BaseApiController
{
    private readonly ISender _mediator = sender;
    [HttpPost("Create")] 
    [CheckPermission(PermissionConstant.DosageForm.Add)]
    public async Task<ActionResult<Result<string>>> Create(CreateDosageFormCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
    [HttpGet("GetAll")]
    [CheckPermission(PermissionConstant.DosageForm.View)]
    public async Task<ActionResult<Result<PaginationResponse<GetDosageFormsResponse>>>> GetAll([FromQuery] GetDosageFormsQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
    [HttpGet("GetById")]
    [CheckPermission(PermissionConstant.DosageForm.View)]
    public async Task<ActionResult<Result<GetDosageFormResponse>>> GetById([FromQuery] GetDosageFormQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
    [HttpPut("Update")]
    [CheckPermission(PermissionConstant.DosageForm.Edit)]
    public async Task<ActionResult<Result<string>>> Update(UpdateDosageFormCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
    [HttpGet("DropDown")]
    [CheckPermission(PermissionConstant.DosageForm.View)]
    public async Task<ActionResult<Result<List<DropDownQueryResponse>>>> DropDown()
    {
        return BaseResponseHandler(await _mediator.Send(new DosageFormsDropDownQuery()));
    }
    [HttpDelete("Delete")]
    [CheckPermission(PermissionConstant.DosageForm.Delete)]
    public async Task<ActionResult<Result<bool>>> Delete(DeleteDosageFormCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }

}