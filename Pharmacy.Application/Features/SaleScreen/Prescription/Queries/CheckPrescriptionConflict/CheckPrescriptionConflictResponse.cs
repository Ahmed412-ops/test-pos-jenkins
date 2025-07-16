using Pharmacy.Application.Services.Abstraction.MedicineConflictService;

namespace Pharmacy.Application.Features.SaleScreen.Prescription.Queries.CheckPrescriptionConflict;

public class CheckPrescriptionConflictResponse(bool hasConflicts, List<DrugInteractionAlert> conflicts)
{
    public bool HasConflicts { get; } = hasConflicts;
    public List<DrugInteractionAlert> Conflicts { get; } = conflicts;
}
