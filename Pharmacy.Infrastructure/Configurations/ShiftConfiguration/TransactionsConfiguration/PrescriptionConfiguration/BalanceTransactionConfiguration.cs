using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Entities.Wallets.Sales;

namespace Pharmacy.Infrastructure.Configurations.ShiftConfiguration.TransactionsConfiguration.PrescriptionConfiguration;

public class BalanceTransactionConfiguration : BaseConfiguration<BalanceTransaction>
{
    public override void Configure(EntityTypeBuilder<BalanceTransaction> builder)
    {
        base.Configure(builder);
        builder
            .HasOne(x => x.Customer)
            .WithMany(x => x.BalanceTransactions)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
        builder
            .HasOne(x => x.RelatedPrescription)
            .WithMany()
            .HasForeignKey(x => x.PrescriptionId)
            .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
        builder.Property(x => x.Amount).HasPrecision(18, 2).IsRequired();
    }
}
