using System.ComponentModel.DataAnnotations.Schema;
using Pharmacy.Domain.Entities.Customers;
using Pharmacy.Domain.Entities.Manufacturers;
using Pharmacy.Domain.Entities.Stock;

namespace Pharmacy.Domain.Entities.Medicine;

public class Medicine : EntityModel
{
    public required string Barcode { get; set; }  
    public Guid ManufacturerId { get; set; }
    public required Manufacturer Manufacturer { get; set; } 
    public Guid? CategoryId { get; set; }
    public MedicineCategory? MedicineCategory { get; set; }
    public Guid DosageFormId { get; set; }
    public required DosageForm.DosageForm DosageForm { get; set; } 
    // Strength– e.g., "500mg" or "5%"
    public required string Strength { get; set; } 
    public string? StorageConditions { get; set; } 
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Index { get; set; } // 5 digits 00001-99999, unique
    public RecorderPointSettings? ReorderSettings { get; set; }
    public bool IsUsedForChildren { get; set; } = false;
    public decimal? DosagePerKgForChildren { get; set; }
    public ICollection<MedicineUnit> MedicineUnits { get; set; } = [];
    public ICollection<MedicineCrossSelling> CrossSellingRecommendations { get; set; } = [];
    public ICollection<MedicineCrossSelling> RecommendedBy { get; set; } = [];
    public ICollection<MedicineEffectiveMaterial> EffectiveMaterials { get; set; } = [];
    public ICollection<MedicineEffectiveMaterialInteraction> DrugInteractions_EM { get; set; } = [];
    public ICollection<MedicineEffectiveMaterialCrossSelling> CrossSelling_EM { get; set; } = [];
    public ICollection<MedicationStock> Stocks { get; set; } = [];
    public ICollection<CustomerChronicMedicine> CustomerChronicMedicines { get; set; } = [];
    public ICollection<StockHistory> StockHistories { get; set; } = [];
}
