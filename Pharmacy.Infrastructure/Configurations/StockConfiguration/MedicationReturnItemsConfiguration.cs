using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Entities.Stock;

namespace Pharmacy.Infrastructure.Configurations.StockConfiguration;

public class MedicationReturnItemsConfiguration : BaseConfiguration<MedicationReturnItem>
    {
        public override void Configure(EntityTypeBuilder<MedicationReturnItem> builder)
        {

            builder.Property(m => m.QuantityToReturn)
                   .IsRequired()
                   .HasPrecision(18, 2); 

            builder.Property(m => m.ReturnValue)
                   .IsRequired()
                   .HasPrecision(18, 2); 

            builder.Property(m => m.ReturnReason)
                   .IsRequired();

            builder.HasOne(m => m.MedicineUnit)
                   .WithMany()
                   .HasForeignKey(m => m.MedicineUnitId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }