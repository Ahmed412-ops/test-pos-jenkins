using MediatR;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.EffectiveMaterial.EffectiveMaterial.Queries.DropDown;

public class EffectiveMaterialsDropDownQuery : IRequest<Result<List<DropDownQueryResponse>>>
{
    
}
