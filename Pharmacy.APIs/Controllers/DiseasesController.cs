using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.APIs.Authorization;
using Pharmacy.Application.Constants.Permissions;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Application.Features.Disease.Disease.Commands.Create;
using Pharmacy.Application.Features.Disease.Disease.Commands.Delete;
using Pharmacy.Application.Features.Disease.Disease.Commands.Update;
using Pharmacy.Application.Features.Disease.Disease.Queries.DropDown;
using Pharmacy.Application.Features.Disease.Disease.Queries.GetAll;
using Pharmacy.Application.Features.Disease.Disease.Queries.GetById;
using Pharmacy.Domain.Dto;

namespace Pharmacy.APIs.Controllers;

[Authorize]

public class DiseasesController(ISender sender) : BaseApiController
{
    private readonly ISender _mediator = sender;
    [HttpPost("Create")]
    [CheckPermission(PermissionConstant.Disease.Add)]
    public async Task<ActionResult<Result<string>>> Create(CreateDiseaseCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
    [HttpGet("GetAll")]
    [CheckPermission(PermissionConstant.Disease.View)]
    public async Task<ActionResult<Result<PaginationResponse<GetDiseasesResponse>>>> GetAll([FromQuery] GetDiseasesQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
    [HttpGet("GetById")]
    [CheckPermission(PermissionConstant.Disease.View)]
    public async Task<ActionResult<Result<GetDiseaseResponse>>> GetById([FromQuery]GetDiseaseQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
    [HttpPut("Update")]
    [CheckPermission(PermissionConstant.Disease.Edit)]
    public async Task<ActionResult<Result<string>>> Update(UpdateDiseaseCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
    [HttpGet("DropDown")]
    [CheckPermission(PermissionConstant.Disease.View)]
    public async Task<ActionResult<Result<List<DropDownQueryResponse>>>> GetDropDown()
    {
        return BaseResponseHandler(await _mediator.Send(new DiseasesDropDownQuery()));
    }
    [HttpDelete("Delete")]
    [CheckPermission(PermissionConstant.Disease.Delete)]
    public async Task<ActionResult<Result<bool>>> Delete([FromQuery] DeleteDiseaseCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
}