using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Stock.Medication.Queries.GetMedicineLevel;

public class GetMedicineLevelQuery : IRequest<Result<List<GetMedicineLevelResponse>>>
{
    public Guid MedicineId { get; set; }
}
