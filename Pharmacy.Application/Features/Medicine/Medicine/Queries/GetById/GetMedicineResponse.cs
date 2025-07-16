using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Application.Features.Medicine.Medicine.Commands.Create;

namespace Pharmacy.Application.Features.Medicine.Medicine.Queries.GetById;

public class GetMedicineResponse : CommonQueryResponseBase
{
    public required string Barcode { get; set; }
    public Guid ManufacturerId { get; set; }
    public required string ManufacturerName { get; set; }
    public Guid CategoryId { get; set; }
    public required string CategoryName { get; set; }
    public Guid DosageFormId { get; set; }
    public required string DosageFormName { get; set; }
    public required string Strength { get; set; }
    public string? StorageConditions { get; set; }
    public bool IsUsedForChildren { get; set; } = false;
    public decimal? DosagePerKgForChildren { get; set; }
    public List<CommonQueryResponseBase> CrossSellingRecommendations { get; set; } = [];
    public List<CommonQueryResponseBase> EffectiveMaterials { get; set; } = [];
    public List<CreateMedicineUnitDto> MedicineUnits { get; set; } = [];
}
