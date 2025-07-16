using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Entities.Medicine;

namespace Pharmacy.Infrastructure.Configurations.MedicinesConfiguration;

public class MedicineCategoryConfiguration : BaseConfiguration<MedicineCategory>
{
    public override void Configure(EntityTypeBuilder<MedicineCategory> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.Name).IsRequired();
        
        builder.HasOne(x => x.ParentCategory)
            .WithMany(c => c.SubCategories)
            .HasForeignKey(x => x.ParentCategoryId);
    }
}
