using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.APIs.Authorization;
using Pharmacy.Application.Constants.Permissions;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Application.Features.Supplier.Commands.Create;
using Pharmacy.Application.Features.Supplier.Commands.Delete;
using Pharmacy.Application.Features.Supplier.Commands.Update;
using Pharmacy.Application.Features.Supplier.Queries.DropDown;
using Pharmacy.Application.Features.Supplier.Queries.GetAll;
using Pharmacy.Application.Features.Supplier.Queries.GetById;
using Pharmacy.Domain.Dto;

namespace Pharmacy.APIs.Controllers;

[Authorize]

public class SuppliersController(ISender sender) : BaseApiController
{
    private readonly ISender _mediator = sender;
    [HttpPost("Create")]
    [CheckPermission(PermissionConstant.Supplier.Add)]
    public async Task<ActionResult<Result<string>>> Create(CreateSupplierCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
    [HttpGet("GetAll")]
    [CheckPermission(PermissionConstant.Supplier.View)]
    public async Task<ActionResult<Result<PaginationResponse<GetSuppliersResponse>>>> GetAll([FromQuery] GetSuppliersQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
    [HttpGet("GetById")]
    [CheckPermission(PermissionConstant.Supplier.View)]
    public async Task<ActionResult<Result<GetSupplierResponse>>> GetById([FromQuery]GetSupplierQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
    [HttpPut("Update")]
    [CheckPermission(PermissionConstant.Supplier.Edit)]
    public async Task<ActionResult<Result<string>>> Update(UpdateSupplierCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
    [HttpGet("DropDown")]
    [CheckPermission(PermissionConstant.Supplier.View)]
    public async Task<ActionResult<Result<List<DropDownQueryResponse>>>> GetDropDown()
    {
        return BaseResponseHandler(await _mediator.Send(new SuppliersDropDownQuery()));
    }
    [HttpDelete("Delete")]
    [CheckPermission(PermissionConstant.Supplier.Delete)]
    public async Task<ActionResult<Result<bool>>> Delete(DeleteSupplierCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
}