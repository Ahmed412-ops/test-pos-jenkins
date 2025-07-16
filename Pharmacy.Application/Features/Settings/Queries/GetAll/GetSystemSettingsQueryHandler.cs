using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Features.Settings.Queries.SystemSettingResponses;
using Pharmacy.Application.Helper.Extensions;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Entities.Settings;
using Pharmacy.Domain.Enum;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
namespace Pharmacy.Application.Features.Settings.Queries.GetAll;
public class GetSystemSettingsQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper
    ) : IRequestHandler<GetSystemSettingsQuery, Result<PaginationResponse<ModuleSettingsResponse>>>
{
    private readonly IGenericRepository<SystemSetting> _repo
        = unitOfWork.GetRepository<SystemSetting>();

    public async Task<Result<PaginationResponse<ModuleSettingsResponse>>> Handle
        (GetSystemSettingsQuery request, CancellationToken cancellationToken)
    {
        var query = await _repo.GetAllQueryableAsync(c => !c.Is_Deleted
          );
        var Count = await query.CountAsync(cancellationToken);
        var pagedEntities = await query
            .Paginate(request)
            .ToListAsync(cancellationToken);

        var grouped = pagedEntities
             .GroupBy(x => x.Module)
             .Select(g => new ModuleSettingsResponse
             {
                 Module = g.Key.ToString(),
                 Settings = g.Select(setting =>
                 {
                     var response = mapper.Map<SystemSettingResponse>(setting);

                     var conversionResult = SettingExtensions.ConvertValue(
                         setting.Value,
                         setting.Type.ToString(),
                         SettingExtensions.GetClrTypeFromSettingType(setting.Type)
                     );

                     response.Value = conversionResult.Succeeded ? conversionResult.Data : null;
                     return response;
                 }).ToList()
             })
             .ToList(); return Result<PaginationResponse<ModuleSettingsResponse>>.Success(
                 new PaginationResponse<ModuleSettingsResponse>
                 {
                     Data = grouped,
                     Count = Count
                 });
    }

}


