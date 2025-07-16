using MediatR;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Uses.Queries.DropDown;

public class UsesDropDownQuery : IRequest<Result<List<DropDownQueryResponse>>>
{
    
}
