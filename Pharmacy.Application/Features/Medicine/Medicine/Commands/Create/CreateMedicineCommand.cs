using MediatR;
using Pharmacy.Application.Dto.Common.Commands;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Medicine.Medicine.Commands.Create;

public class CreateMedicineCommand : IBaseCommand, IRequest<Result<string>>
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public required string Barcode { get; set; }
    public Guid ManufacturerId { get; set; }
    public Guid? CategoryId { get; set; }
    public Guid DosageFormId { get; set; }
    public required string Strength { get; set; }
    public string? StorageConditions { get; set; }
    public bool IsUsedForChildren { get; set; }
    public decimal? DosagePerKgForChildren { get; set; }
    public List<Guid> CrossSellingRecommendations { get; set; } = [];
    public List<Guid> EffectiveMaterials { get; set; } = [];
    public List<CreateMedicineUnitDto> MedicineUnits { get; set; } = [];
}

public class CreateMedicineUnitDto
{
    public Guid UnitId { get; set; }
    public bool CanBeSold { get; set; }
    public bool IsDefault { get; set; }
    public bool CalcUnit { get; set; }
    public decimal QuantityForCalcUnit { get; set; }
}
