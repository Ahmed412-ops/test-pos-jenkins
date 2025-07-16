using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.APIs.Authorization;
using Pharmacy.Application.Constants.Permissions;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Commands.AddTransaction;
using Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Commands.CompleteInvoices;
using Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Commands.Create;
using Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Commands.Delete;
using Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Commands.DeleteTransaction;
using Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Commands.Review;
using Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Commands.Update;
using Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Queries.DropDown;
using Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Queries.GetAll;
using Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Queries.GetById;
using Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Queries.GetTransactions;
using Pharmacy.Domain.Dto;

namespace Pharmacy.APIs.Controllers;

[Authorize]

public class SupplierInvoicesController(ISender sender) : BaseApiController
{
    private readonly ISender _mediator = sender;
    [HttpPost("Create")] 
    [CheckPermission(PermissionConstant.SupplierInvoice.Add)]
    public async Task<ActionResult<Result<string>>> Create(CreateSupplierInvoiceCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
    [HttpGet("GetAll")]
    [CheckPermission(PermissionConstant.SupplierInvoice.View)]
    public async Task<ActionResult<Result<InvoicesPaginationResponse>>> GetAll([FromQuery] GetSupplierInvoicesQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
    [HttpGet("GetById")]
    [CheckPermission(PermissionConstant.SupplierInvoice.View)]
    public async Task<ActionResult<Result<GetSupplierInvoiceResponse>>> GetById([FromQuery] GetSupplierInvoiceQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
    [HttpPut("Update")]
    [CheckPermission(PermissionConstant.SupplierInvoice.Edit)]
    public async Task<ActionResult<Result<string>>> Update(UpdateSupplierInvoiceCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
    [HttpGet("DropDown")]
    [CheckPermission(PermissionConstant.SupplierInvoice.View)]
    public async Task<ActionResult<Result<List<DropDownQueryResponse>>>> GetDropDown()
    {
        return BaseResponseHandler(await _mediator.Send(new SupplierInvoicesDropDownQuery()));
    }
    [HttpDelete("DeleteInvoice")]
    [CheckPermission(PermissionConstant.SupplierInvoice.Delete)]
    public async Task<ActionResult<Result<bool>>> Delete(DeleteSupplierInvoiceCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
    [HttpPost("AddTransaction")]
    [CheckPermission(PermissionConstant.SupplierInvoice.Add)]
    public async Task<ActionResult<Result<string>>> AddTransaction(AddTransactionCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
    [HttpGet("GetTransactions")]
    [CheckPermission(PermissionConstant.SupplierInvoice.View)]
    public async Task<ActionResult<Result<PaginationResponse<GetTransactionsResponse>>>> GetTransactions([FromQuery] GetTransactionsQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
    [HttpPut("CompleteInvoices")]
    [CheckPermission(PermissionConstant.SupplierInvoice.Edit)]
    public async Task<ActionResult<Result<string>>> CompleteInvoices(CompleteInvoicesCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
    [HttpDelete("DeleteTransaction")]
    [CheckPermission(PermissionConstant.SupplierInvoice.Delete)]
    public async Task<ActionResult<Result<bool>>> DeleteTransaction(DeleteTransactionCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
    [HttpPost("ReviewInvoice")]
    [CheckPermission(PermissionConstant.SupplierInvoice.Edit)]
    public async Task<ActionResult<Result<string>>> ReviewInvoice(ReviewSupplierInvoiceCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
}
