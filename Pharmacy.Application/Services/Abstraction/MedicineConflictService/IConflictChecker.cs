
namespace Pharmacy.Application.Services.Abstraction.MedicineConflictService;

public interface IConflictChecker
{
    Task<List<DrugInteractionAlert>> CheckPrescriptionConflictsAsync(
        List<Guid> MedicineIds,
        Guid NewMedicineId,
        List<Guid>? customerDiseasesIds = null
    );
}
