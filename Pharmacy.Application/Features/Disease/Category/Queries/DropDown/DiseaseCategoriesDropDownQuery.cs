using MediatR;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Disease.Category.Queries.DropDown;

public class DiseaseCategoriesDropDownQuery : IRequest<Result<List<DropDownQueryResponse>>>
{
    
}
