using MediatR;
using Pharmacy.Application.Features.Settings.Queries.SystemSettingResponses;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Enum;

namespace Pharmacy.Application.Features.Settings.Queries.GetByModule;

public class GetSettingsByModuleQuery : IRequest<Result<List<SystemSettingResponse>>>
{
    public SettingsModules Module { get; set; } 
}

