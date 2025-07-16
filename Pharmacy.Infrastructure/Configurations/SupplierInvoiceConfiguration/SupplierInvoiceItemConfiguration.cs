using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Entities.SupplierInvoice;

namespace Pharmacy.Infrastructure.Configurations.SupplierInvoiceConfiguration
{
    public class SupplierInvoiceItemConfiguration : BaseConfiguration<SupplierInvoiceItem>
    {
        public override void Configure(EntityTypeBuilder<SupplierInvoiceItem> builder)
        {
            base.Configure(builder);

            builder.HasOne(x => x.SupplierInvoice)
                .WithMany(si => si.InvoiceItems)
                .HasForeignKey(x => x.SupplierInvoiceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.MedicineUnit)
                .WithMany() 
                .HasForeignKey(x => x.MedicineUnitId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.PublicSellingPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.Quantity)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.TaxPercentage)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.SupplierDiscountPercentage)
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0);

            builder.Property(x => x.ExpiryDate)
                .IsRequired();
        }
    }
}
