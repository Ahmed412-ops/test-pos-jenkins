using System.Linq.Expressions;

namespace Pharmacy.Application.Features.SaleScreen.Medicine.Queries.GetInfoById;

public static class MedicineDtoMapper
{
    public static Expression<
        Func<Domain.Entities.Stock.MedicationStock, GetMedicineInfoResponse>
    > MedicineInfoProjection() =>
        m => new GetMedicineInfoResponse
        {
            MedicineId = m.MedicineId,
            Name = m.Medicine.Name,
            Id = m.Id,
            ExpiryDate = m.ExpiryDate,
            PurchasePrice = m.PurchasePrice,
            SellingPrice = m.SellingPrice,
            GeneratedBarcode = m.GeneratedBarcode,
            Barcode = m.Medicine.Barcode,
            CategoryName = m.Medicine.MedicineCategory!.Name,
            DosageFormName = m.Medicine.DosageForm.Name,
            Strength = m.Medicine.Strength,
            StorageConditions = m.Medicine.StorageConditions,
            IsUsedForChildren = m.Medicine.IsUsedForChildren,
            DosagePerKgForChildren = m.Medicine.DosagePerKgForChildren,
            CrossSellingRecommendations = m
                .Medicine.CrossSellingRecommendations.Select(cr => new QueryResponseBase
                {
                    Id = cr.Id,
                    Name = cr.CrossSellingMedicine.Name,
                })
                .ToList(),
            DrugInteractions_EM = m
                .Medicine.DrugInteractions_EM.Select(di => new QueryResponseBase
                {
                    Id = di.Id,
                    Name = di.EffectiveMaterial!.Name,
                })
                .ToList(),
            CrossSelling_EM = m
                .Medicine.CrossSelling_EM.Select(cr => new QueryResponseBase
                {
                    Id = cr.Id,
                    Name = cr.EffectiveMaterial!.Name,
                })
                .ToList(),
            MedicineUnits = m
                .Medicine.MedicineUnits.Select(mu => new MedicineInfoUnitDto
                {
                    Id = mu.Id,
                    UnitId = mu.UnitId,
                    UnitName = mu.Unit.Name,
                    CanBeSold = mu.CanBeSold,
                    IsDefault = mu.IsDefault,
                    CalcUnit = mu.CalcUnit,
                    QuantityForCalcUnit = mu.QuantityForCalcUnit,
                })
                .ToList(),
            EffectiveMaterials = m
                .Medicine.EffectiveMaterials.Select(em => new EffectiveMaterialDto
                {
                    Id = em.EffectiveMaterial!.Id,
                    Name = em.EffectiveMaterial.Name,
                    CategoryName = em.EffectiveMaterial.Category.Name,
                    PatientInformation_En = em.EffectiveMaterial.PatientInformation_En,
                    PatientInformation_Ar = em.EffectiveMaterial.PatientInformation_Ar,
                    BlackBoxWarning = em.EffectiveMaterial.BlackBoxWarning,
                    IsChronic = em.EffectiveMaterial.IsChronic,
                    CommonUses = em
                        .EffectiveMaterial.CommonUses.Select(use => new QueryResponseBase
                        {
                            Id = use.Id,
                            Name = use.Use!.Name,
                        })
                        .ToList(),
                    OffLabelUses = em
                        .EffectiveMaterial.OffLabelUses.Select(use => new QueryResponseBase
                        {
                            Id = use.Id,
                            Name = use.Use!.Name,
                        })
                        .ToList(),
                    SideEffects = em
                        .EffectiveMaterial.SideEffects.Select(se => new QueryResponseBase
                        {
                            Id = se.Id,
                            Name = se.SideEffect!.Name,
                        })
                        .ToList(),
                    FoodInteractions = em
                        .EffectiveMaterial.FoodInteractions.Select(fi => new QueryResponseBase
                        {
                            Id = fi.Id,
                            Name = fi.Food!.Name,
                        })
                        .ToList(),
                })
                .ToList(),
        };
}
