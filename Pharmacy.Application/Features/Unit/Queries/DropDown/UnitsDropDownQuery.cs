using MediatR;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Unit.Queries.DropDown;

public class UnitsDropDownQuery : IRequest<Result<List<DropDownQueryResponse>>>
{
    
}
