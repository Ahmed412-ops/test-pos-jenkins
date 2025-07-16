using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.APIs.Authorization;
using Pharmacy.Application.Constants.Permissions;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Application.Features.Wallet.Commands.Create;
using Pharmacy.Application.Features.Wallet.Commands.Update;
using Pharmacy.Application.Features.Wallet.Queries.DropDown;
using Pharmacy.Application.Features.Wallet.Queries.GetAll;
using Pharmacy.Application.Features.Wallet.Queries.GetById;
using Pharmacy.Domain.Dto;

namespace Pharmacy.APIs.Controllers;

[Authorize]
public class WalletsController(ISender sender) : BaseApiController
{
    private readonly ISender _mediator = sender;
    [HttpPost("Create")]
    [CheckPermission(PermissionConstant.Wallet.Add)]
    public async Task<ActionResult<Result<string>>> Create(CreateWalletCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
    [HttpGet("GetAll")]
    [CheckPermission(PermissionConstant.Wallet.View)]
    public async Task<ActionResult<Result<PaginationResponse<GetWalletsResponse>>>> GetAll([FromQuery] GetWalletsQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
    [HttpGet("GetById")]
    [CheckPermission(PermissionConstant.Wallet.View)]
    public async Task<ActionResult<Result<GetWalletResponse>>> GetById([FromQuery]GetWalletQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
    [HttpPut("Update")]
    [CheckPermission(PermissionConstant.Wallet.Edit)]
    public async Task<ActionResult<Result<string>>> Update(UpdateWalletCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }
    [HttpGet("DropDown")]
    [CheckPermission(PermissionConstant.Wallet.View)]
    public async Task<ActionResult<Result<List<DropDownQueryResponse>>>> GetDropDown()
    {
        return BaseResponseHandler(await _mediator.Send(new WalletsDropDownQuery()));
    }
}
