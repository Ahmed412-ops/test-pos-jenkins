using MediatR;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.SideEffect.Queries.DropDown;

public class SideEffectsDropDownQuery : IRequest<Result<List<DropDownQueryResponse>>>
{
    
}
