namespace Pharmacy.Application.Services.Abstraction.MedicineConflictService;

public record DrugInteractionAlert(
    Guid DrugId,
    List<string> Alerts
);
