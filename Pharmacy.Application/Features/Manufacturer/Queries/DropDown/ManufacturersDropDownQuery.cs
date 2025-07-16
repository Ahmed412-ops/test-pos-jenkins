using MediatR;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Manufacturer.Queries.DropDown;

public class ManufacturersDropDownQuery : IRequest<Result<List<DropDownQueryResponse>>>
{
    
}
