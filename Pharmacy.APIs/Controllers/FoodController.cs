using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Application.Features.Food.Queries.GetAll;
using Pharmacy.Application.Features.Food.Commands.Create;
using Pharmacy.Application.Features.Food.Commands.Update;
using Pharmacy.Application.Features.Food.Queries.GetById;
using Pharmacy.Domain.Dto;
using Pharmacy.Application.Features.Food.Queries.DropDown;
using Pharmacy.Application.Features.Food.Commands.Delete;
using Pharmacy.Application.Constants.Permissions;
using Pharmacy.APIs.Authorization;

namespace Pharmacy.APIs.Controllers;

[Authorize]

public class FoodController(ISender sender) : BaseApiController
{
    private readonly ISender _mediator = sender;
    [HttpPost("Create")]
    [CheckPermission(PermissionConstant.Food.Add)]
    public async Task<ActionResult<Result<string>>> Create(CreateFoodCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
    [HttpGet("GetAll")]
    [CheckPermission(PermissionConstant.DiseaseCategory.View)]
    public async Task<ActionResult<Result<PaginationResponse<GetFoodResponse>>>> GetAll([FromQuery] GetFoodQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
    [HttpGet("GetById")]
    [CheckPermission(PermissionConstant.DiseaseCategory.View)]
    public async Task<ActionResult<Result<GetFoodByIdResponse>>> GetById([FromQuery] GetFoodByIdQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
    [HttpPut("Update")]
    [CheckPermission(PermissionConstant.DiseaseCategory.Edit)]
    public async Task<ActionResult<Result<string>>> Update(UpdateFoodCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
    [HttpGet("DropDown")]
    [CheckPermission(PermissionConstant.DiseaseCategory.View)]
    public async Task<ActionResult<Result<List<DropDownQueryResponse>>>> DropDown()
    {
        return BaseResponseHandler(await _mediator.Send(new FoodDropDownQuery()));
    }
    [HttpDelete("Delete")]
    [CheckPermission(PermissionConstant.DiseaseCategory.Delete)]
    public async Task<ActionResult<Result<bool>>> Delete([FromQuery] DeleteFoodCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }

}