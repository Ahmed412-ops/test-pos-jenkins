using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Entities.Wallets.Sales;

namespace Pharmacy.Infrastructure.Configurations.ShiftConfiguration.TransactionsConfiguration.PrescriptionConfiguration;

public class PrescriptionTransactionConfiguration : BaseConfiguration<PrescriptionTransaction>
{
    public override void Configure(EntityTypeBuilder<PrescriptionTransaction> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.InvoiceNumber).IsRequired().HasMaxLength(50).ValueGeneratedOnAdd();

        builder
            .HasOne(x => x.ShiftWallet)
            .WithMany(x => x.PrescriptionTransactions)
            .HasForeignKey(x => x.ShiftWalletId);

        builder
            .HasOne(x => x.Prescription)
            .WithMany(x => x.Transactions)
            .HasForeignKey(x => x.PrescriptionId)
            .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
    }
}
