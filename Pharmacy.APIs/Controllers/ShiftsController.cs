using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.APIs.Authorization;
using Pharmacy.Application.Constants.Permissions;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Application.Features.Shifts.Commands.Close;
using Pharmacy.Application.Features.Shifts.Commands.Create;
using Pharmacy.Application.Features.Shifts.Queries.DropDown;
using Pharmacy.Application.Features.Shifts.Queries.GetAll;
using Pharmacy.Application.Features.Shifts.Queries.GetById;
using Pharmacy.Application.Features.Shifts.Queries.GetOpenedShifts;
using Pharmacy.Domain.Dto;

namespace Pharmacy.APIs.Controllers;

[AllowAnonymous]
public class ShiftsController(ISender sender) : BaseApiController
{
    private readonly ISender _mediator = sender;
    [HttpPost("Open")]
    [CheckPermission(PermissionConstant.Shift.Add)]
    public async Task<ActionResult<Result<string>>> OpenShift(OpenShiftCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
    [HttpPut("Close")]
    [CheckPermission(PermissionConstant.Shift.Add)]
    public async Task<ActionResult<Result<string>>> CloseShift(CloseShiftCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
    [HttpGet("GetAll")]
    [CheckPermission(PermissionConstant.Shift.View)]
    public async Task<ActionResult<Result<PaginationResponse<GetShiftsResponse>>>> GetAll([FromQuery] GetShiftsQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
    [HttpGet("GetById")]
    [CheckPermission(PermissionConstant.Shift.View)]
    public async Task<ActionResult<Result<GetShiftResponse>>> GetById([FromQuery] GetShiftQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
    [HttpGet("CurrentWalletsDropDown")]
    [CheckPermission(PermissionConstant.Shift.View)]
    public async Task<ActionResult<Result<List<CurrentWalletsDropDownResponse>>>> GetDropDown()
    {
        return BaseResponseHandler(await _mediator.Send(new CurrentWalletsDropDownQuery()));
    }
    [HttpGet("OpenedShiftsDropDown")]
    [CheckPermission(PermissionConstant.Shift.View)]
    public async Task<ActionResult<Result<List<GetOpenedShiftsResponse>>>> GetOpenedShiftsDropDown()
    {
        return BaseResponseHandler(await _mediator.Send(new GetOpenedShiftsQuery()));
    }

    // [HttpGet("TransactionsHistory")]
    // [CheckPermission(PermissionConstant.Shift.View)]
    // public async Task<ActionResult<Result<PaginationResponse<GetCashTransactionsHistoryResponse>>>> GetTransactionsHistory([FromQuery] GetCashTransactionsHistoryQuery query)
    // {
    //     return BaseResponseHandler(await _mediator.Send(query));
    // }

}
