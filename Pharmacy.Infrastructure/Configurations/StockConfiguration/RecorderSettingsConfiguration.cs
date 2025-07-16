using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Entities.Stock;

namespace Pharmacy.Infrastructure.Configurations.StockConfiguration;

public class RecorderSettingsConfiguration : BaseConfiguration<RecorderPointSettings>
{
    public override void Configure(EntityTypeBuilder<RecorderPointSettings> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.MedicineId).IsRequired();
        builder.Property(x => x.ReorderPoint).IsRequired();
        builder.Property(x => x.RestockingQuantity).IsRequired();

        builder.HasOne(x => x.Medicine)
               .WithOne(s => s.ReorderSettings)
               .HasForeignKey<RecorderPointSettings>(x => x.MedicineId)
               .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(x => x.PreferredSupplier)
               .WithMany()
               .HasForeignKey(x => x.PreferredSupplierId)
               .OnDelete(DeleteBehavior.SetNull);
        
    }
}

