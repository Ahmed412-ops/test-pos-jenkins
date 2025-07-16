using MediatR;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Disease.Disease.Queries.DropDown;

public class DiseasesDropDownQuery :  IRequest<Result<List<DropDownQueryResponse>>>
{
    
}
