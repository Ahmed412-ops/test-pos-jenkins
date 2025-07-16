using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.APIs.Authorization;
using Pharmacy.Application.Constants.Permissions;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Features.Stock.History.Queries.GetAll;
using Pharmacy.Domain.Dto;

namespace Pharmacy.APIs.Controllers;

[Authorize]
public class MedicationStockHistoriesController(ISender sender) : BaseApiController
{
    private readonly ISender _mediator = sender;
    [HttpGet("GetAll")]
    [CheckPermission(PermissionConstant.MedicationStock.View)]
    public async Task<ActionResult<Result<PaginationResponse<GetStockHistoryResponse>>>> GetAll([FromQuery] GetStockHistoryQuery query)
    {
        return BaseResponseHandler(await _mediator.Send(query));
    }
}
