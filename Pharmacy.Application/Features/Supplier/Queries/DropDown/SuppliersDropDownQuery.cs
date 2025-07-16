using MediatR;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Supplier.Queries.DropDown;

public class SuppliersDropDownQuery : IRequest<Result<List<DropDownQueryResponse>>>
{
    
}
