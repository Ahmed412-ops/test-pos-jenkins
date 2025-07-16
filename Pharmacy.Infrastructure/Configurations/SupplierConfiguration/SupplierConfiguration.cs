using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Entities.Supplier;

namespace Pharmacy.Infrastructure.Configurations.SupplierConfiguration;

public class SupplierConfiguration : BaseConfiguration<Supplier>
{
    public override void Configure(EntityTypeBuilder<Supplier> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.SupplierType).IsRequired();
        builder.Property(x => x.Name).IsRequired();

        builder.HasMany(x => x.Contacts)
               .WithOne(x => x.Supplier)
               .HasForeignKey(x => x.SupplierId)
               .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(x => x.PurchaseOrders)
                .WithOne(x => x.Supplier)
                .HasForeignKey(x => x.SupplierId);
    }
}
