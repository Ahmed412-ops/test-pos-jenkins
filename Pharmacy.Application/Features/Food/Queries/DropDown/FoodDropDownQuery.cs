using MediatR;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Food.Queries.DropDown;

public class FoodDropDownQuery : IRequest<Result<List<DropDownQueryResponse>>>
{
    
}
