using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Entities.Stock;

namespace Pharmacy.Infrastructure.Configurations.StockConfiguration;

  public class MedicationReturnConfiguration : BaseConfiguration<MedicationReturn>
    {
        public override void Configure(EntityTypeBuilder<MedicationReturn> builder)
        {

            builder.Property(mr => mr.ReturnReferenceNumber)
                .IsRequired()
                .HasMaxLength(50); 

            builder.Property(mr => mr.ReturnDate)
                .IsRequired();

            builder.Property(mr => mr.ReturnStatus)
                .IsRequired();

            builder.HasOne(mr => mr.Supplier)
                .WithMany()
                .HasForeignKey(mr => mr.SupplierId)
                .OnDelete(DeleteBehavior.Restrict); 

            builder.HasOne(mr => mr.SupplierInvoice)
                .WithMany()
                .HasForeignKey(mr => mr.SupplierInvoiceId)
                .OnDelete(DeleteBehavior.SetNull); 

            builder.HasMany(mr => mr.ReturnItems)
                .WithOne(mri => mri.MedicationReturn)
                .HasForeignKey(mri => mri.MedicationReturnId)
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
