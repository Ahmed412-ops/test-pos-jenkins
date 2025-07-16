using MediatR;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Roles.Queries.RolesDropDown;

public class RolesDropDownQuery : IRequest<Result<List<DropDownQueryResponse>>>
{

}
