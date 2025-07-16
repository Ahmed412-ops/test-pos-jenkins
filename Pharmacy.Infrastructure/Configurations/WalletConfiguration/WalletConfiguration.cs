namespace Pharmacy.Infrastructure.Configurations.WalletConfiguration;

public class WalletConfiguration : BaseConfiguration<Domain.Entities.Wallets.Wallet>
{
    public override void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.Entities.Wallets.Wallet> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.Type).IsRequired();
        builder.Property(x => x.Name).IsRequired();
        
    }
}
