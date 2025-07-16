using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.APIs.Authorization;
using Pharmacy.Application.Constants.Permissions;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Features.Stock.Return.Commands.Create;
using Pharmacy.Application.Features.Stock.Return.Commands.SetStatus;
using Pharmacy.Application.Features.Stock.Return.Commands.Update;
using Pharmacy.Application.Features.Stock.Return.Queries.GetAll;
using Pharmacy.Application.Features.Stock.Return.Queries.GetById;
using Pharmacy.Domain.Dto;

namespace Pharmacy.APIs.Controllers;

[Authorize]
public class MedicationReturnsController(ISender sender) : BaseApiController
{
    private readonly ISender _mediator = sender;
    [HttpPost("Create")]
    [CheckPermission(PermissionConstant.MedicationReturn.Add)]
    public async Task<ActionResult<Result<string>>> Create(CreateMedicationReturnCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
    [HttpGet("GetAll")]
    [CheckPermission(PermissionConstant.MedicationReturn.View)]
    public async Task<ActionResult<Result<PaginationResponse<GetMedicationReturnsResponse>>>> GetAll([FromQuery] GetMedicationReturnsQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
    [HttpGet("GetById")]
    [CheckPermission(PermissionConstant.MedicationReturn.View)]
    public async Task<ActionResult<Result<GetMedicationReturnResponse>>> GetById([FromQuery] GetMedicationReturnQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
    [HttpPut("Update")]
    [CheckPermission(PermissionConstant.MedicationReturn.Edit)]
    public async Task<ActionResult<Result<string>>> Update(UpdateReturnStockCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
    [HttpPut("SetStatus")]
    [CheckPermission(PermissionConstant.MedicationReturn.Edit)]
    public async Task<ActionResult<Result<bool>>> SetStatus(SetStatusCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
    // [HttpDelete("Delete")]
    // public async Task<ActionResult<Result<bool>> Delete(DeleteReturnStockCommand command)
    // {
    //     return BaseResponseHandler(await _mediator.Send(command));
    // }
}
