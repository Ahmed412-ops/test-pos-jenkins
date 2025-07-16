using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.APIs.Authorization;
using Pharmacy.Application.Constants.Permissions;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Application.Features.Disease.Category.Commands.Create;
using Pharmacy.Application.Features.Disease.Category.Commands.Delete;
using Pharmacy.Application.Features.Disease.Category.Commands.Update;
using Pharmacy.Application.Features.Disease.Category.Queries.DropDown;
using Pharmacy.Application.Features.Disease.Category.Queries.GetAll;
using Pharmacy.Application.Features.Disease.Category.Queries.GetById;
using Pharmacy.Domain.Dto;

namespace Pharmacy.APIs.Controllers;

[Authorize]

public class DiseaseCategoriesController(ISender sender) : BaseApiController
{
    private readonly ISender _mediator = sender;

    [HttpPost("Create")]
    [CheckPermission(PermissionConstant.DiseaseCategory.Add)]
    public async Task<ActionResult<Result<string>>> Create(CreateDiseaseCategoryCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
    [CheckPermission(PermissionConstant.DiseaseCategory.View)]
    [HttpGet("GetAll")]
    public async Task<ActionResult<Result<PaginationResponse<GetDiseaseCategoriesResponse>>>> GetAll([FromQuery] GetDiseaseCategoriesQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
    [CheckPermission(PermissionConstant.DiseaseCategory.View)]
    [HttpGet("GetById")]
    public async Task<ActionResult<Result<GetDiseaseCategoryResponse>>> GetById([FromQuery] GetDiseaseCategoryQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
    [CheckPermission(PermissionConstant.DiseaseCategory.Edit)]
    [HttpPut("Update")]
    public async Task<ActionResult<Result<string>>> Update(UpdateDiseaseCategoryCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
    [CheckPermission(PermissionConstant.DiseaseCategory.View)]
    [HttpGet("DropDown")]
    public async Task<ActionResult<Result<List<DropDownQueryResponse>>>> GetDropDown()
    {
        return BaseResponseHandler(await _mediator.Send(new DiseaseCategoriesDropDownQuery()));
    }
    [CheckPermission(PermissionConstant.DiseaseCategory.Delete)]
    [HttpDelete("Delete")]
    public async Task<ActionResult<Result<bool>>> Delete([FromQuery] DeleteDiseaseCategoryCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
}