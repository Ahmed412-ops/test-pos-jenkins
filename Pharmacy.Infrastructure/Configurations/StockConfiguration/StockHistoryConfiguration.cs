using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Entities.Stock;

namespace Pharmacy.Infrastructure.Configurations.StockConfiguration;

public class StockHistoryConfiguration : BaseConfiguration<StockHistory>
{
    public override void Configure(EntityTypeBuilder<StockHistory> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.MedicineId).IsRequired();
        builder.Property(x => x.TransactionType).IsRequired();
        builder.Property(x => x.TransactionDate).IsRequired();
        builder.Property(x => x.PerformedById).IsRequired();
        builder.Property(x => x.QuantityChange).IsRequired();
        builder.Property(x => x.UpdatedStockLevel).IsRequired();
        

    }
}
