using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.APIs.Authorization;
using Pharmacy.Application.Constants.Permissions;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Features.Shifts.CashExpenses.Commands.Create;
using Pharmacy.Application.Features.Shifts.CashExpenses.Commands.Delete;
using Pharmacy.Application.Features.Shifts.CashExpenses.Commands.Update;
using Pharmacy.Application.Features.Shifts.CashExpenses.Queries.GetAll;
using Pharmacy.Application.Features.Shifts.CashExpenses.Queries.GetById;
using Pharmacy.Domain.Dto;

namespace Pharmacy.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CashExpensesController(ISender sender) : BaseApiController
    {
        private readonly ISender _mediator = sender;
        [HttpPost("Create")]
        [CheckPermission(PermissionConstant.CashExpense.Add)]
        public async Task<ActionResult<Result<string>>> Create(CreateCashExpenseCommand command)
        {
            return BaseResponseHandler(await _mediator.Send(command));
        }
        [HttpGet("GetAll")]
        [CheckPermission(PermissionConstant.CashExpense.View)]
        public async Task<ActionResult<Result<PaginationResponse<GetCashExpensesResponse>>>> GetAll([FromQuery] GetCashExpensesQuery query)
        {
            return BaseResponseHandler(await _mediator.Send(query));
        }
        [HttpGet("GetById")]
        [CheckPermission(PermissionConstant.CashExpense.View)]
        public async Task<ActionResult<Result<GetCashExpenseResponse>>> GetById([FromQuery] GetCashExpenseQuery query)
        {
            return BaseResponseHandler(await _mediator.Send(query));
        }
        [HttpPut("Update")]
        [CheckPermission(PermissionConstant.CashExpense.Edit)]
        public async Task<ActionResult<Result<string>>> Update(UpdateCashExpenseCommand command)
        {
            return BaseResponseHandler(await _mediator.Send(command));
        }
        [HttpDelete("Delete")]
        [CheckPermission(PermissionConstant.CashExpense.Delete)]
        public async Task<ActionResult<Result<bool>>> Delete([FromQuery] DeleteCashExpenseCommand command)
        {
            return BaseResponseHandler(await _mediator.Send(command));
        }

    }
}
