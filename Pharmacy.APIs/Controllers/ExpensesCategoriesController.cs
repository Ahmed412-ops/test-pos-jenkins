using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.APIs.Authorization;
using Pharmacy.Application.Constants.Permissions;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Application.Features.Expense.Category.Commands.ChangeActivation;
using Pharmacy.Application.Features.Expense.Category.Commands.Create;
using Pharmacy.Application.Features.Expense.Category.Commands.Delete;
using Pharmacy.Application.Features.Expense.Category.Commands.Update;
using Pharmacy.Application.Features.Expense.Category.Queries.DropDown;
using Pharmacy.Application.Features.Expense.Category.Queries.GetAll;
using Pharmacy.Application.Features.Expense.Category.Queries.GetById;
using Pharmacy.Domain.Dto;

namespace Pharmacy.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesCategoriesController(ISender sender) : BaseApiController
    {
        private readonly ISender _mediator = sender;
        [HttpPost("Create")]
        [CheckPermission(PermissionConstant.ExpenseCategory.Add)]
        public async Task<ActionResult<Result<string>>> Create(CreateExpenseCategoryCommand command)
        {
            return BaseResponseHandler(await _mediator.Send(command));
        }
        [HttpGet("GetAll")]
        [CheckPermission(PermissionConstant.ExpenseCategory.View)]
        public async Task<ActionResult<Result<PaginationResponse<GetExpensesCategoriesResponse>>>> GetAll([FromQuery] GetExpensesCategoriesQuery query)
        {
            return BaseResponseHandler(await _mediator.Send(query));
        }
        [HttpGet("GetById")]
        [CheckPermission(PermissionConstant.ExpenseCategory.View)]
        public async Task<ActionResult<Result<GetExpenseCategoryResponse>>> GetById([FromQuery] GetExpenseCategoryQuery query)
        {
            return BaseResponseHandler(await _mediator.Send(query));
        }
        [HttpGet("DropDown")]
        [CheckPermission(PermissionConstant.ExpenseCategory.View)]
        public async Task<ActionResult<Result<List<DropDownQueryResponse>>>> GetDropDown()
        {
            return BaseResponseHandler(await _mediator.Send(new ExpenseCategoryDropDownQuery()));
        }
        [HttpPut("ChangeActivation")]
        [CheckPermission(PermissionConstant.ExpenseCategory.Edit)]
        public async Task<ActionResult<Result<bool>>> ChangeActivation([FromBody] ChangeExpenseCategoryActivationCommand command)
        {
            return BaseResponseHandler(await _mediator.Send(command));
        }
        [HttpPut("Update")]
        [CheckPermission(PermissionConstant.ExpenseCategory.Edit)]
        public async Task<ActionResult<Result<string>>> Update(UpdateExpenseCategoryCommand command)
        {
            return BaseResponseHandler(await _mediator.Send(command));
        }
        [HttpDelete("Delete")]
        [CheckPermission(PermissionConstant.ExpenseCategory.Delete)]
        public async Task<ActionResult<Result<bool>>> Delete([FromQuery] DeleteExpenseCategoryCommand command)
        {
            return BaseResponseHandler(await _mediator.Send(command));
        }

    }
}
