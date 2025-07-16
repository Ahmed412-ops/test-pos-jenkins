using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Entities.Wallets.Return;

namespace Pharmacy.Infrastructure.Configurations.ShiftConfiguration.TransactionsConfiguration.ReturnConfiguration;

public class ReturnConfiguration : BaseConfiguration<Return>
{
    public override void Configure(EntityTypeBuilder<Return> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Notes).IsRequired(false);

        builder
            .HasOne(x => x.Prescription)
            .WithMany(x => x.Returns)
            .HasForeignKey(x => x.PrescriptionId)
            .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);

        builder
            .HasOne(x => x.ShiftWallet)
            .WithMany(x => x.ReturnTransactions)
            .HasForeignKey(x => x.ShiftWalletId);

        builder
            .HasMany(x => x.Items)
            .WithOne(x => x.Return)
            .HasForeignKey(x => x.ReturnId)
            .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
    }
}
