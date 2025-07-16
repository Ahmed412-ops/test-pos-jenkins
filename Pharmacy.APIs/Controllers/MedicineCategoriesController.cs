using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.APIs.Authorization;
using Pharmacy.Application.Constants.Permissions;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Features.Medicine.MedicinesCategory.Commands.Create;
using Pharmacy.Application.Features.Medicine.MedicinesCategory.Commands.Delete;
using Pharmacy.Application.Features.Medicine.MedicinesCategory.Commands.Update;
using Pharmacy.Application.Features.Medicine.MedicinesCategory.Queries.DropDown;
using Pharmacy.Application.Features.Medicine.MedicinesCategory.Queries.GetAll;
using Pharmacy.Application.Features.Medicine.MedicinesCategory.Queries.GetById;
using Pharmacy.Domain.Dto;

namespace Pharmacy.APIs.Controllers;

[Authorize]

public class MedicineCategoriesController(ISender sender) : BaseApiController
{
    private readonly ISender _mediator = sender;
    [HttpPost("Create")]
    [CheckPermission(PermissionConstant.MedicineCategory.Add)]
    public async Task<ActionResult<Result<string>>> Create(CreateMedicineCategoryCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
    [HttpGet("GetAll")]
    [CheckPermission(PermissionConstant.MedicineCategory.View)]
    public async Task<ActionResult<Result<PaginationResponse<GetMedicineCategoriesResponse>>>> GetAll([FromQuery] GetMedicinesCategoriesQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
    [HttpGet("GetById")]
    [CheckPermission(PermissionConstant.MedicineCategory.View)]
    public async Task<ActionResult<Result<GetMedicineCategoryResponse>>> GetById([FromQuery] GetMedicineCategoryQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
    [HttpPut("Update")]
    [CheckPermission(PermissionConstant.MedicineCategory.Edit)]
    public async Task<ActionResult<Result<string>>> Update(UpdateMedicineCategoryCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
    [HttpGet("DropDown")]
    [CheckPermission(PermissionConstant.MedicineCategory.View)]
    public async Task<ActionResult<Result<List<MedicineCategoriesDropDownResponse>>>> GetAllDropDown()
    {
        return BaseResponseHandler(await _mediator.Send(new MedicineCategoriesDropDownQuery()));
    }
    [HttpDelete("Delete")]
    [CheckPermission(PermissionConstant.MedicineCategory.Delete)]
    public async Task<ActionResult<Result<bool>>> Delete([FromQuery] DeleteMedicineCategoryCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
}
