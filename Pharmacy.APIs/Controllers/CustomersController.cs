using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.APIs.Authorization;
using Pharmacy.Application.Constants.Permissions;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Application.Features.Customer.Commands.Create;
using Pharmacy.Application.Features.Customer.Commands.Delete;
using Pharmacy.Application.Features.Customer.Commands.Update;
using Pharmacy.Application.Features.Customer.Queries.DropDown;
using Pharmacy.Application.Features.Customer.Queries.GetAll;
using Pharmacy.Application.Features.Customer.Queries.GetById;
using Pharmacy.Application.Features.Customer.Queries.GetById.GetByIdResponses;
using Pharmacy.Application.Features.Customer.Queries.SearchCustomer;
using Pharmacy.Domain.Dto;

namespace Pharmacy.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController(ISender sender) : BaseApiController
    {
        private readonly ISender _mediator = sender;
        [HttpPost("Create")]
        [CheckPermission(PermissionConstant.Customer.Add)]
        public async Task<ActionResult<Result<CreateCustomerResponse>>> Create(CreateCustomerCommand command)
        {
            return BaseResponseHandler(await _mediator.Send(command));
        }
        [HttpGet("GetAll")]
        [CheckPermission(PermissionConstant.Customer.View)]
        public async Task<ActionResult<Result<PaginationResponse<GetCustomersResponse>>>> GetAll([FromQuery] GetCustomersQuery query)
        {
            return BaseResponseHandler(await _mediator.Send(query));
        }
        [HttpGet("GetById")]
        [CheckPermission(PermissionConstant.Customer.View)]
        public async Task<ActionResult<Result<GetCustomerByIdResponse>>> GetById([FromQuery] GetCustomerByIdQuery query)
        {
            return BaseResponseHandler(await _mediator.Send(query));
        }
        [HttpGet("SearchCustomer")]
        [CheckPermission(PermissionConstant.Customer.View)]
        public async Task<ActionResult<Result<List<SearchCustomerResponse>>>> SearchCustomer([FromQuery] SearchCustomerQuery query)
        {
            return BaseResponseHandler(await _mediator.Send(query));
        }
        [HttpPut("Update")]
        [CheckPermission(PermissionConstant.Customer.Edit)]
        public async Task<ActionResult<Result<CreateCustomerResponse>>> Update(UpdateCustomerCommand command)
        {
            return BaseResponseHandler(await _mediator.Send(command));
        }
        [HttpGet("DropDown")]
        [CheckPermission(PermissionConstant.Customer.View)]
        public async Task<ActionResult<Result<List<DropDownQueryResponse>>>> GetDropDown()
        {
            return BaseResponseHandler(await _mediator.Send(new CustomerDropDownQuery()));
        }
        [HttpDelete("Delete")]
        [CheckPermission(PermissionConstant.Customer.Delete)]
        public async Task<ActionResult<Result<bool>>> Delete([FromQuery] DeleteCustomerCommand command)
        {
            return BaseResponseHandler(await _mediator.Send(command));
        }
    }
}
