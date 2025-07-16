using MediatR;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.DosageForm.Queries.DropDown;

public class DosageFormsDropDownQuery : IRequest<Result<List<DropDownQueryResponse>>>
{
    
}
