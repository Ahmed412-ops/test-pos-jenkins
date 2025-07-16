using MediatR;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.EffectiveMaterial.Category.Queries.DropDown;

public class EffectiveCategoriesDropDownQuery : IRequest<Result<List<DropDownQueryResponse>>>
{
    
}
