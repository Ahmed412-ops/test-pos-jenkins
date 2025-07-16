using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Entities.Wallets.Sales;

namespace Pharmacy.Infrastructure.Configurations.ShiftConfiguration.TransactionsConfiguration.PrescriptionConfiguration;

public class PrescriptionItemsConfiguration :  BaseConfiguration<PrescriptionItem>
{
    public override void Configure(EntityTypeBuilder<PrescriptionItem> builder)
    {
        base.Configure(builder);

        builder
            .HasOne(x => x.Prescription)
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.PrescriptionId)
            .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);

        builder
            .HasOne(x => x.MedicationStock)
            .WithMany()
            .HasForeignKey(x => x.MedicationStockId)
            .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);

        builder
            .HasOne(x => x.MedicineUnit)
            .WithMany()
            .HasForeignKey(x => x.MedicineUnitId)
            .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
    }
}
