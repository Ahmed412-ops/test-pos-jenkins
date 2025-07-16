using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Stock.Medication.Queries.GetById;

public class GetMedicationQuery: IRequest<Result<GetMedicationResponse>>
{
    public Guid Id { get; set; }    
}
