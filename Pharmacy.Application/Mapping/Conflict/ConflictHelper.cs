using Pharmacy.Application.Services.Abstraction.MedicineConflictService;

namespace Pharmacy.Application.Mapping.Conflict;

public static class ConflictHelper
{
    public static void AddAlert(
        this Dictionary<Guid, DrugInteractionAlert> mainDict,
        List<DrugInteractionAlert> newAlerts
    )
    {
        foreach (var alert in newAlerts)
        {
            if (mainDict.TryGetValue(alert.DrugId, out var existing))
                existing.Alerts.AddRange(alert.Alerts);
            else
                mainDict[alert.DrugId] = alert;
        }
    }

    public static void AddAlert(
        this Dictionary<Guid, DrugInteractionAlert> alertDict,
        Guid medicineId,
        string message
    )
    {
        if (!alertDict.TryGetValue(medicineId, out var alert))
        {
            alert = new DrugInteractionAlert(medicineId, []);
            alertDict[medicineId] = alert;
        }
        alert.Alerts.Add(message);
    }
}
