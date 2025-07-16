using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Entities.Medicine;

namespace Pharmacy.Infrastructure.Configurations.MedicinesConfiguration;

public class MedicineCrossSellingConfiguration : BaseConfiguration<MedicineCrossSelling>
{
    public override void Configure(EntityTypeBuilder<MedicineCrossSelling> builder)
    {
        base.Configure(builder);

        builder.HasOne(x => x.Medicine)
            .WithMany(m => m.RecommendedBy)  
            .HasForeignKey(x => x.MedicineId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(x => x.CrossSellingMedicine)
            .WithMany(m => m.CrossSellingRecommendations)  
            .HasForeignKey(x => x.CrossSellingMedicineId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
