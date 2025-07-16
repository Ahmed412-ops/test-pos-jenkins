using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.APIs.Authorization;
using Pharmacy.Application.Constants.Permissions;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Features.Settings.Commands.Delete;
using Pharmacy.Application.Features.Settings.Commands.Update;
using Pharmacy.Application.Features.Settings.Queries;
using Pharmacy.Application.Features.Settings.Queries.GetAll;
using Pharmacy.Application.Features.Settings.Queries.GetByModule;
using Pharmacy.Application.Features.Settings.Queries.SystemSettingResponses;
using Pharmacy.Domain.Dto;

namespace Pharmacy.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController(ISender sender) : BaseApiController
    {
        private readonly ISender _mediator = sender;
        [HttpGet("GetAll")]
        [CheckPermission(PermissionConstant.Settings.View)]
        public async Task<ActionResult<Result<PaginationResponse<ModuleSettingsResponse>>>> GetAll([FromQuery] GetSystemSettingsQuery query)
        {
            return BaseResponseHandler(await _mediator.Send(query));
        }
        [HttpGet("GetByModule")]
        [CheckPermission(PermissionConstant.Settings.View)]
        public async Task<ActionResult<Result<List<SystemSettingResponse>>>> GetByModule([FromQuery] GetSettingsByModuleQuery query)
        {
            return BaseResponseHandler(await _mediator.Send(query));
        }
        [HttpPut("Update")]
        [CheckPermission(PermissionConstant.Settings.Edit)]
        public async Task<ActionResult<Result<string>>> Update(UpdateSystemSettingCommand command)
        {
            return BaseResponseHandler(await _mediator.Send(command));
        }
        [HttpDelete("Delete")]
        [CheckPermission(PermissionConstant.Settings.Delete)]
        public async Task<ActionResult<Result<bool>>> Delete([FromQuery] DeleteSystemSettingCommand command)
        {
            return BaseResponseHandler(await _mediator.Send(command));
        }
    }
}
