using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.APIs.Authorization;
using Pharmacy.Application.Constants.Permissions;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Application.Features.EffectiveMaterial.Category.Commands.Create;
using Pharmacy.Application.Features.EffectiveMaterial.Category.Commands.Delete;
using Pharmacy.Application.Features.EffectiveMaterial.Category.Commands.Update;
using Pharmacy.Application.Features.EffectiveMaterial.Category.Queries.DropDown;
using Pharmacy.Application.Features.EffectiveMaterial.Category.Queries.GetAll;
using Pharmacy.Application.Features.EffectiveMaterial.Category.Queries.GetById;
using Pharmacy.Domain.Dto;

namespace Pharmacy.APIs.Controllers;

[Authorize]

public class EffectiveMaterialCategoriesController(ISender sender) : BaseApiController
{
    private readonly ISender _mediator = sender;
    [HttpPost("Create")]
    [CheckPermission(PermissionConstant.EffectiveMaterialCategory.Add)]
    public async Task<ActionResult<Result<string>>> Create(CreateEffectiveMaterialCategoryCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
    [HttpGet("GetAll")]
    [CheckPermission(PermissionConstant.EffectiveMaterialCategory.View)]
    public async Task<ActionResult<Result<PaginationResponse<GetEffectiveMaterialCategoriesResponse>>>> GetAll([FromQuery] GetEffectiveMaterialCategoriesQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
    [HttpGet("GetById")]
    [CheckPermission(PermissionConstant.DiseaseCategory.View)]
    public async Task<ActionResult<Result<GetEffectiveMaterialCategoryResponse>>> GetById([FromQuery]GetEffectiveMaterialCategoryQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
    [HttpPut("Update")]
    [CheckPermission(PermissionConstant.EffectiveMaterialCategory.Edit)]
    public async Task<ActionResult<Result<string>>> Update(UpdateEffectiveMaterialCategoryCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
    [HttpGet("DropDown")]
    [CheckPermission(PermissionConstant.EffectiveMaterialCategory.View)]
    public async Task<ActionResult<Result<List<DropDownQueryResponse>>>> GetDropDown()
    {
        return BaseResponseHandler(await _mediator.Send(new EffectiveCategoriesDropDownQuery()));
    }
    [HttpDelete("Delete")]
    [CheckPermission(PermissionConstant.EffectiveMaterialCategory.Delete)]
    public async Task<ActionResult<Result<bool>>> Delete(DeleteEffectiveMaterialCategoryCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }

}
