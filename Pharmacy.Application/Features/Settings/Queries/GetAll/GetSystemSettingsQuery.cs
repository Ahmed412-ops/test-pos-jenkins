using MediatR;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Features.Settings.Queries.SystemSettingResponses;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Settings.Queries.GetAll;

public class GetSystemSettingsQuery :  Pagination, IRequest<Result<PaginationResponse<ModuleSettingsResponse>>>;

