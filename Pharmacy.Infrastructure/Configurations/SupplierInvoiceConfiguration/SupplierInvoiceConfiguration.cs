using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Entities.SupplierInvoice;

namespace Pharmacy.Infrastructure.Configurations.SupplierInvoiceConfiguration
{
    public class SupplierInvoiceConfiguration : BaseConfiguration<SupplierInvoice>
    {
        public override void Configure(EntityTypeBuilder<SupplierInvoice> builder)
        {
            base.Configure(builder);

            // Invoice Number: required, unique, with a max length constraint
            builder.Property(x => x.InvoiceNumber)
                .IsRequired()
                .HasMaxLength(50);
            builder.HasIndex(x => x.InvoiceNumber)
                .IsUnique();

            builder.Property(x => x.InvoiceDate)
                .IsRequired();

            builder.Property(x => x.PaymentStatus)
                .IsRequired();

            builder.HasOne(x => x.Supplier)
                .WithMany(s => s.SupplierInvoices)
                .HasForeignKey(x => x.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.PurchaseOrder)
                .WithMany() 
                .HasForeignKey(x => x.PurchaseOrderId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(x => x.InvoiceItems)
                .WithOne(i => i.SupplierInvoice)
                .HasForeignKey(i => i.SupplierInvoiceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.ShippingFees)
                .HasColumnType("decimal(18,2)");
            builder.Property(x => x.AmountPaid)
                .HasColumnType("decimal(18,2)");
            builder.Property(x => x.FinalInvoiceTotal)
                .HasColumnType("decimal(18,2)");
        }
    }
}
