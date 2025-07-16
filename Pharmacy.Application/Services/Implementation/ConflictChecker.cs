using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Mapping.Conflict;
using Pharmacy.Application.Services.Abstraction.MedicineConflictService;
using Pharmacy.Domain.Entities.EffectiveMaterial;
using Pharmacy.Domain.Entities.Medicine;

namespace Pharmacy.Application.Services.Implementation;

public class ConflictChecker(IUnitOfWork unitOfWork) : IConflictChecker
{
    private readonly IGenericRepository<Medicine> _medicineRepo =
        unitOfWork.GetRepository<Medicine>();
    private readonly IGenericRepository<EffectiveMaterialDrugInteraction> _interactionRepo =
        unitOfWork.GetRepository<EffectiveMaterialDrugInteraction>();
    private readonly IGenericRepository<MedicineEffectiveMaterialInteraction> _medMaterialInteractionRepo =
        unitOfWork.GetRepository<MedicineEffectiveMaterialInteraction>();
    private readonly IGenericRepository<EffectiveMaterialDisease> _materialDiseaseRepo =
        unitOfWork.GetRepository<EffectiveMaterialDisease>();

    public async Task<List<DrugInteractionAlert>> CheckPrescriptionConflictsAsync(
        List<Guid> MedicineIds,
        Guid NewMedicineId,
        List<Guid>? customerDiseaseIds = null
    )
    {
        var newMedicine = await _medicineRepo.FindAsync(
            m => m.Id == NewMedicineId && m.DosageForm.AffectsDrugInteractions,
            select: m => new
            {
                m.Id,
                m.Name,
                EffectiveMaterials = m
                    .EffectiveMaterials.Where(em => em.EffectiveMaterial != null)
                    .Select(em => new { em.EffectiveMaterialId, em.EffectiveMaterial!.Name })
                    .ToList(),
            }
        );

        if (newMedicine == null || newMedicine.EffectiveMaterials.Count == 0)
            return [];

        var newMedicineMaterials = newMedicine.EffectiveMaterials.ToDictionary(
            m => m.EffectiveMaterialId,
            m => m.Name
        );

        var directInteractionMaterials = await _medMaterialInteractionRepo.GetAllAsync(
            mi => mi.MedicineId == NewMedicineId,
            select: mi => mi.EffectiveMaterialId
        );

        var existingMedicines = await _medicineRepo.GetAllAsync(
            m => MedicineIds.Contains(m.Id) && m.DosageForm.AffectsDrugInteractions,
            select: m => new
            {
                m.Id,
                m.Name,
                EffectiveMaterials = m
                    .EffectiveMaterials.Where(em => em.EffectiveMaterial != null)
                    .Select(em => new { em.EffectiveMaterialId, em.EffectiveMaterial!.Name })
                    .ToList(),
            }
        );

        var materialMedicineLookup = existingMedicines
            .SelectMany(
                med => med.EffectiveMaterials,
                (medicine, material) =>
                    new { material.EffectiveMaterialId, Medicine = (dynamic)medicine }
            )
            .ToLookup(x => x.EffectiveMaterialId, x => x.Medicine);

        var alertDict = await CheckMaterialInteractions(
            newMedicine,
            newMedicineMaterials,
            materialMedicineLookup
        );

        CheckDirectInteractions(
            newMedicine,
            directInteractionMaterials,
            materialMedicineLookup,
            alertDict
        );

        if (customerDiseaseIds?.Count > 0)
        {
            var diseaseAlerts = await CheckDiseaseConflicts(customerDiseaseIds, newMedicine);
            alertDict.AddAlert(diseaseAlerts);
        }

        return alertDict.Values.ToList();
    }

    private async Task<Dictionary<Guid, DrugInteractionAlert>> CheckMaterialInteractions(
        dynamic newMedicine,
        Dictionary<Guid, string> newMedicineMaterials,
        ILookup<Guid, dynamic> materialMedicineLookup
    )
    {
        var alertDict = new Dictionary<Guid, DrugInteractionAlert>();
        var interactions = await _interactionRepo.GetAllQueryableAsync(i =>
            newMedicineMaterials.Keys.Contains(i.EffectiveMaterialId)
            || newMedicineMaterials.Keys.Contains(i.InteractingMaterialId)
        );

        foreach (var interaction in interactions)
        {
            Guid newMatId,
                otherMatId;
            if (newMedicineMaterials.ContainsKey(interaction.EffectiveMaterialId))
            {
                newMatId = interaction.EffectiveMaterialId;
                otherMatId = interaction.InteractingMaterialId;
            }
            else if (newMedicineMaterials.ContainsKey(interaction.InteractingMaterialId))
            {
                newMatId = interaction.InteractingMaterialId;
                otherMatId = interaction.EffectiveMaterialId;
            }
            else
                continue;

            if (!materialMedicineLookup.Contains(otherMatId))
                continue;

            var newMatName = newMedicineMaterials[newMatId];
            foreach (var existingMed in materialMedicineLookup[otherMatId])
            {
                var existingMat = ((IEnumerable<dynamic>)existingMed.EffectiveMaterials).First(m =>
                    m.EffectiveMaterialId == otherMatId
                );

                alertDict.AddAlert(
                    (Guid)existingMed.Id,
                    $"Material interaction: {newMatName} in {newMedicine.Name} "
                        + $"with {existingMat.Name} in {existingMed.Name}"
                );
            }
        }
        return alertDict;
    }

    private static void CheckDirectInteractions(
        dynamic newMedicine,
        List<Guid> directInteractionMaterials,
        ILookup<Guid, dynamic> materialMedicineLookup,
        Dictionary<Guid, DrugInteractionAlert> alertDict
    )
    {
        foreach (var materialId in directInteractionMaterials)
        {
            if (!materialMedicineLookup.Contains(materialId))
                continue;

            foreach (var existingMed in materialMedicineLookup[materialId])
            {
                var effectiveMaterials = (IEnumerable<dynamic>)existingMed.EffectiveMaterials;
                var existingMat = effectiveMaterials.First(m =>
                    m.EffectiveMaterialId == materialId
                );

                alertDict.AddAlert(
                    (Guid)existingMed.Id,
                    $"Direct medicine interaction: {newMedicine.Name} "
                        + $"with {existingMat.Name} in {existingMed.Name}"
                );
            }
        }
    }

    private async Task<List<DrugInteractionAlert>> CheckDiseaseConflicts(
        List<Guid> diseaseIds,
        dynamic newMedicine
    )
    {
        var alerts = new List<DrugInteractionAlert>();

        var contraindicatedMaterials = await _materialDiseaseRepo.GetAllAsync(
            md => diseaseIds.Contains(md.DiseaseId),
            select: md => md.EffectiveMaterialId
        );

        if (contraindicatedMaterials.Count == 0)
            return alerts;

        if (newMedicine == null || newMedicine!.EffectiveMaterials == null)
            return alerts;

        // Collect conflict material IDs
        var conflictIds = new List<Guid>();
        foreach (var em in newMedicine!.EffectiveMaterials)
        {
            conflictIds.Add(em.EffectiveMaterialId);
        }

        var conflicts = conflictIds.Intersect(contraindicatedMaterials).ToList();

        if (conflicts.Count > 0)
        {
            alerts.Add(
                new DrugInteractionAlert(
                    (Guid)newMedicine.Id,
                    [
                        $"The new medicine '{newMedicine.Name}' contains {conflicts.Count} "
                        + "effective material(s) that conflict with the customer's medical conditions."
                    ]
                )
            );
        }

        return alerts;
    }

}
