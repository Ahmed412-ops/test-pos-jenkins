using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Entities.Stock;

namespace Pharmacy.Infrastructure.Configurations.StockConfiguration;

public class MedicationStockConfiguration : BaseConfiguration<MedicationStock>
{
    public override void Configure(EntityTypeBuilder<MedicationStock> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.MedicineId).IsRequired();
        builder.Property(x => x.Quantity).IsRequired();
        builder.Property(x => x.ExpiryDate).IsRequired();
        builder.Property(x => x.SellingPrice).IsRequired();
        builder.Property(x => x.GeneratedBarcode).IsRequired();

        builder.HasOne(x => x.Medicine)
               .WithMany(s => s.Stocks)
               .HasForeignKey(x => x.MedicineId)
               .OnDelete(DeleteBehavior.Cascade);

    }
}
