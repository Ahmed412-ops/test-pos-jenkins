using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Entities.Wallets.Sales;

namespace Pharmacy.Infrastructure.Configurations.ShiftConfiguration.TransactionsConfiguration.PrescriptionConfiguration;

public class PrescriptionConfiguration : BaseConfiguration<Prescription>
{
    public override void Configure(EntityTypeBuilder<Prescription> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.Notes).IsRequired(false);
        builder.Property(x => x.InvoiceNumber).IsRequired().HasMaxLength(50).ValueGeneratedOnAdd();

        builder
            .HasOne(x => x.Shift)
            .WithMany(x => x.Prescriptions)
            .HasForeignKey(x => x.ShiftId);

        builder
            .HasOne(x => x.Customer)
            .WithMany(x => x.Prescriptions)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);

        builder
            .HasMany(x => x.Items)
            .WithOne(x => x.Prescription)
            .HasForeignKey(x => x.PrescriptionId)
            .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
    }
}
