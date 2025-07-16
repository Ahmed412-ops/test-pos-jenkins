using MediatR;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Customer.Queries.DropDown;

public class CustomerDropDownQuery : IRequest<Result<List<DropDownQueryResponse>>>
{
}
