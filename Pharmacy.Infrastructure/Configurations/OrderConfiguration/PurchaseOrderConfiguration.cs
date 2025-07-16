using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Entities.Order;

namespace Pharmacy.Infrastructure.Configurations.OrderConfiguration;

public class PurchaseOrderConfiguration : BaseConfiguration<PurchaseOrder>
{
    public override void Configure(EntityTypeBuilder<PurchaseOrder> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.PurchaseOrderNumber)
               .IsRequired();
        builder.Property(x => x.OrderDate)
               .IsRequired();
        
        builder.HasOne(x => x.Supplier)
               .WithMany(s => s.PurchaseOrders) 
               .HasForeignKey(x => x.SupplierId);
        
        builder.HasMany(x => x.Items)
               .WithOne(x => x.PurchaseOrder)
               .HasForeignKey(x => x.PurchaseOrderId);
    }
}

