using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.APIs.Authorization;
using Pharmacy.Application.Constants.Permissions;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Features.SaleScreen.Medicine.Queries.GetInfoById;
using Pharmacy.Application.Features.Stock.Medication.Commands.AdjustQuantity;
using Pharmacy.Application.Features.Stock.Medication.Commands.Create;
using Pharmacy.Application.Features.Stock.Medication.Commands.Delete;
using Pharmacy.Application.Features.Stock.Medication.Commands.Update;
using Pharmacy.Application.Features.Stock.Medication.Queries.GetAll;
using Pharmacy.Application.Features.Stock.Medication.Queries.GetById;
using Pharmacy.Application.Features.Stock.Medication.Queries.GetLevel;
using Pharmacy.Application.Features.Stock.Medication.Queries.GetMedicineLevel;
using Pharmacy.Application.Features.Stock.Medication.Queries.GetNearExpiryMedicines;
using Pharmacy.Domain.Dto;

namespace Pharmacy.APIs.Controllers;

[Authorize]
public class MedicationsStockController(ISender sender) : BaseApiController
{
    private readonly ISender _mediator = sender;
    [HttpPost("Create")]
    [CheckPermission(PermissionConstant.MedicationStock.Add)]
    public async Task<ActionResult<Result<string>>> Create(CreateMedicationStockCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
    [HttpGet("GetAll")]
    [CheckPermission(PermissionConstant.MedicationStock.View)]
    public async Task<ActionResult<Result<PaginationResponse<GetMedicationsResponse>>>> GetAll([FromQuery] GetMedicationsQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
    [HttpGet("GetById")]
    [CheckPermission(PermissionConstant.MedicationStock.View)]
    public async Task<ActionResult<Result<GetMedicationResponse>>> GetById([FromQuery] GetMedicationQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
    [HttpPut("Update")]
    [CheckPermission(PermissionConstant.MedicationStock.Edit)]
    public async Task<ActionResult<Result<string>>> Update(UpdateMedicationStockCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
    [HttpGet("GetStockLevels")]
    [CheckPermission(PermissionConstant.MedicationStock.View)]
    public async Task<ActionResult<Result<PaginationResponse<GetLevelsResponse>>>> GetLevels([FromQuery] GetLevelsQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
    [HttpGet("GetMedicineLevel")]
    [CheckPermission(PermissionConstant.MedicationStock.View)]
    public async Task<ActionResult<Result<List<GetMedicineLevelResponse>>>> GetMedicineLevel([FromQuery] GetMedicineLevelQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
    [HttpPut("AdjustQuantity")]
    [CheckPermission(PermissionConstant.MedicationStock.Edit)]
    public async Task<ActionResult<Result<string>>> AdjustQuantity(AdjustQuantityCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
    [HttpDelete("Delete")]
    [CheckPermission(PermissionConstant.MedicationStock.Delete)]
    public async Task<ActionResult<Result<bool>>> Delete(DeleteMedicationStockCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }

    [HttpGet("Near-Expiry")]
    [CheckPermission(PermissionConstant.Medicine.View)]
    public async Task<ActionResult<Result<PaginationResponse<GetNearExpiryMedicinesResponse>>>> GetNearExpiryMedicines(
    [FromQuery] GetNearExpiryMedicinesQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
    [HttpGet("GetMedicineInfo")]
    [CheckPermission(PermissionConstant.Medicine.View)]
    public async Task<ActionResult<Result<GetMedicineInfoResponse>>> GetMedicineInfo([FromQuery] GetMedicineInfoQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
}
