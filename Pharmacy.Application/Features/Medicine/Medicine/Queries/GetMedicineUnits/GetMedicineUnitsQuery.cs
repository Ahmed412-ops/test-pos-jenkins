using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Medicine.Medicine.Queries.GetMedicineUnits;

public class GetMedicineUnitsQuery : IRequest<Result<List<GetMedicineUnitsResponse>>>
{
    
}
