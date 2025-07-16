using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Features.Settings.Queries.SystemSettingResponses;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Entities.Settings;

namespace Pharmacy.Application.Features.Settings.Queries.GetByModule
{
    internal class GetSettingsByModuleQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) :
        BaseHandler<GetSettingsByModuleQuery, Result<List<SystemSettingResponse>>>
    {
        private readonly IGenericRepository<SystemSetting> _systemSetting
            = unitOfWork.GetRepository<SystemSetting>();
        public override async Task<Result<List<SystemSettingResponse>>> Handle(GetSettingsByModuleQuery request, CancellationToken cancellationToken)
        {
            var setting = await _systemSetting
                .GetAllAsync(x => x.Module == request.Module);

            if (setting == null)
                return Result<List<SystemSettingResponse>>.Fail(Messages.ModuleNotFound);

            var result = mapper.Map<List<SystemSettingResponse>>(setting);
            return Result<List<SystemSettingResponse>>.Success(result);
        }
    }
}
