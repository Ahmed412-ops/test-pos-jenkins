using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Entities.Medicine;

namespace Pharmacy.Infrastructure.Configurations.MedicinesConfiguration
{
    public class MedicineConfiguration : BaseConfiguration<Medicine>
    {
        public override void Configure(EntityTypeBuilder<Medicine> builder)
        {
            base.Configure(builder);

            // Required fields
            builder.Property(x => x.Barcode).IsRequired();
            builder.Property(x => x.Strength).IsRequired();

            // Manufacturer (One-to-Many)
            builder
                .HasOne(x => x.Manufacturer)
                .WithMany(m => m.Medicines)
                .HasForeignKey(x => x.ManufacturerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Medicine Category (One-to-Many)
            builder
                .HasOne(x => x.MedicineCategory)
                .WithMany(mc => mc.Medicines)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Dosage Form (One-to-Many)
            builder
                .HasOne(x => x.DosageForm)
                .WithMany(df => df.Medicines)
                .HasForeignKey(x => x.DosageFormId)
                .OnDelete(DeleteBehavior.Restrict);

            // Effective Materials (One-to-Many)
            builder
                .HasMany(m => m.EffectiveMaterials)
                .WithOne(em => em.Medicine)
                .HasForeignKey(em => em.MedicineId)
                .OnDelete(DeleteBehavior.Cascade);

            // Allowed Selling Units (Many-to-Many via join entity MedicineUnit)
            builder
                .HasMany(m => m.MedicineUnits)
                .WithOne(mu => mu.Medicine)
                .HasForeignKey(mu => mu.MedicineId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.DosagePerKgForChildren).HasPrecision(4, 2).IsRequired(false);

            builder.Property(m => m.Index).ValueGeneratedOnAdd();

            builder.HasIndex(m => m.Index).IsUnique();
        }
    }
}
