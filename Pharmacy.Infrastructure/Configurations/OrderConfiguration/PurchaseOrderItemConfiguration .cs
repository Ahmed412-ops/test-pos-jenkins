using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Entities.Order;

namespace Pharmacy.Infrastructure.Configurations.OrderConfiguration;

public class PurchaseOrderItemConfiguration : BaseConfiguration<PurchaseOrderItem>
{
    public override void Configure(EntityTypeBuilder<PurchaseOrderItem> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Quantity).IsRequired();

        builder
            .HasOne(x => x.MedicineUnit)
            .WithMany()
            .HasForeignKey(x => x.MedicineUnitId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
