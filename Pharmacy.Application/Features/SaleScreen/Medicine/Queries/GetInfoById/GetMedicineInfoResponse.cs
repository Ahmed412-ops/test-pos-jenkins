using Pharmacy.Application.Dto.Common.Queries;

namespace Pharmacy.Application.Features.SaleScreen.Medicine.Queries.GetInfoById;

public class GetMedicineInfoResponse : CommonQueryResponseBase
{
    public DateOnly ExpiryDate { get; set; }
    public Guid MedicineId { get; set; }
    public decimal? PurchasePrice { get; set; }
    public decimal SellingPrice { get; set; }
    public string GeneratedBarcode { get; set; } = string.Empty;
    public required string Barcode { get; set; }
    public required string CategoryName { get; set; }
    public required string DosageFormName { get; set; }
    public required string Strength { get; set; }
    public string? StorageConditions { get; set; }
    public bool IsUsedForChildren { get; set; }
    public decimal? DosagePerKgForChildren { get; set; }
    public decimal? CalculatedDose { get; set; }
    public List<QueryResponseBase> CrossSellingRecommendations { get; set; } = [];
    public List<QueryResponseBase> DrugInteractions_EM { get; set; } = [];
    public List<QueryResponseBase> CrossSelling_EM { get; set; } = [];
    public List<MedicineInfoUnitDto> MedicineUnits { get; set; } = [];
    public List<EffectiveMaterialDto> EffectiveMaterials { get; set; } = [];
}

public class QueryResponseBase
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class EffectiveMaterialDto : QueryResponseBase
{
    public string CategoryName { get; set; } = string.Empty;
    public string? PatientInformation_En { get; set; }
    public string? PatientInformation_Ar { get; set; }
    public string? BlackBoxWarning { get; set; }
    public bool IsChronic { get; set; } = false;
    public List<QueryResponseBase> CommonUses { get; set; } = [];
    public List<QueryResponseBase> OffLabelUses { get; set; } = [];
    public List<QueryResponseBase> SideEffects { get; set; } = [];
    public List<QueryResponseBase> FoodInteractions { get; set; } = [];
}
public class MedicineInfoUnitDto
{
    public Guid Id { get; set; }
    public Guid UnitId { get; set; }
    public string UnitName { get; set; } = string.Empty;
    public bool CanBeSold { get; set; }
    public bool IsDefault { get; set; }
    public bool CalcUnit { get; set; }
    public decimal QuantityForCalcUnit { get; set; }
}
