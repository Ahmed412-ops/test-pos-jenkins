using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Entities.Medicine;

namespace Pharmacy.Infrastructure.Configurations.MedicinesConfiguration
{
    public class MedicineUnitConfiguration : BaseConfiguration<MedicineUnit>
    {
        public override void Configure(EntityTypeBuilder<MedicineUnit> builder)
        {
            base.Configure(builder);
            
            // Relationship to Unit
            builder.HasOne(mu => mu.Unit)
                   .WithMany(u => u.Medicines)
                   .HasForeignKey(mu => mu.UnitId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Relationship to Medicine (AllowedSellingUnits)
            builder.HasOne(mu => mu.Medicine)
                   .WithMany(m => m.MedicineUnits)
                   .HasForeignKey(mu => mu.MedicineId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
