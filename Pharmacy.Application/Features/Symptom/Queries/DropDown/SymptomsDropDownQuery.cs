using MediatR;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Symptom.Queries.DropDown;

public class SymptomsDropDownQuery : IRequest<Result<List<DropDownQueryResponse>>>
{
    
}
