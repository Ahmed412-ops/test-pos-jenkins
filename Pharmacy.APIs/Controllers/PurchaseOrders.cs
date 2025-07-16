using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.APIs.Authorization;
using Pharmacy.Application.Constants.Permissions;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Features.Order.Commands.ChangeOrderStatus;
using Pharmacy.Application.Features.Order.Commands.Create;
using Pharmacy.Application.Features.Order.Commands.Update;
using Pharmacy.Application.Features.Order.Queries.DropDown;
using Pharmacy.Application.Features.Order.Queries.GetAll;
using Pharmacy.Application.Features.Order.Queries.GetById;
using Pharmacy.Domain.Dto;

namespace Pharmacy.APIs.Controllers;

[Authorize]

public class PurchaseOrdersController(ISender sender) : BaseApiController
{
    private readonly ISender _mediator = sender;
    [HttpPost("Create")] 
    [CheckPermission(PermissionConstant.PurchaseOrder.Add)]
    public async Task<ActionResult<Result<string>>> Create(CreateOrderCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
    [HttpGet("GetAll")]
    [CheckPermission(PermissionConstant.PurchaseOrder.View)]
    public async Task<ActionResult<Result<PaginationResponse<GetOrdersResponse>>>> GetAll([FromQuery] GetOrdersQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
    [HttpGet("GetById")]
    [CheckPermission(PermissionConstant.PurchaseOrder.View)]
    public async Task<ActionResult<Result<GetOrderResponse>>> GetById([FromQuery]GetOrderQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
    [HttpPut("Update")]
    [CheckPermission(PermissionConstant.PurchaseOrder.Edit)]
    public async Task<ActionResult<Result<string>>> Update(UpdateOrderCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
    [HttpPut("ChangeStatus")]
    [CheckPermission(PermissionConstant.PurchaseOrder.Edit)]
    public async Task<ActionResult<Result<bool>>> ChangeStatus(ChangeOrderStatusCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
    [HttpGet("DropDown")]
    [CheckPermission(PermissionConstant.PurchaseOrder.View)]
    public async Task<ActionResult<Result<List<OrdersDropDownQueryResponse>>>> GetDropDown()
    {
        return BaseResponseHandler(await _mediator.Send(new OrdersDropDownQuery()));
    }
    // [HttpDelete("Delete")]
    // public async Task<ActionResult<Result<bool>>> Delete(DeletePurchaseOrderCommand command)
    // {
    //     return BaseResponseHandler(await _mediator.Send(command));
    // }
}