using Pharmacy.Domain.Enum;

namespace Pharmacy.Domain.Entities.Medicine;

public class MedicineCategory : EntityModel
{
    public Guid? ParentCategoryId { get; set; }
    public MedicineCategory? ParentCategory { get; set; }
    public ICollection<MedicineCategory> SubCategories { get; set; } = [];
    public ICollection<Medicine> Medicines { get; set; } = [];
    public CategoryLevel Level
    {
        get
        {
            if (ParentCategory == null)
                return CategoryLevel.Main;
            else if (ParentCategory.ParentCategory == null)
                return CategoryLevel.Sub;
            else
                return CategoryLevel.SubSub;
        }
    }

}
