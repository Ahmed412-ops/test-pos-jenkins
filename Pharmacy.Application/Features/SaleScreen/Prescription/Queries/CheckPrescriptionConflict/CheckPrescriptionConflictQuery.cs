using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.SaleScreen.Prescription.Queries.CheckPrescriptionConflict;

public class CheckMedicineConflictQuery : IRequest<Result<CheckPrescriptionConflictResponse>>
{
    public Guid? CustomerId { get; set; }
    public List<Guid> ExistingMedicineIds { get; set; } = [];
    public Guid NewMedicineId { get; set; }
}
