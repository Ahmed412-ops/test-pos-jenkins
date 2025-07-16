namespace Pharmacy.Infrastructure.Configurations.ShiftConfiguration;

public class ShiftWalletConfiguration :BaseConfiguration<Domain.Entities.Wallets.ShiftWallet>
{
    public override void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.Entities.Wallets.ShiftWallet> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.OpeningBalance).IsRequired();
        builder.HasOne(x => x.Shift)
            .WithMany(x => x.ShiftWallets)
            .HasForeignKey(x => x.ShiftId)
            .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
        builder.HasOne(x => x.Wallet)
            .WithMany(x => x.ShiftWallets)
            .HasForeignKey(x => x.WalletId)
            .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
    }
}
