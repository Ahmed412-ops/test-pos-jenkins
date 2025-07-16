using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Pharmacy.Infrastructure.Configurations.RefreshTokenConfiguration;

public class RefreshTokenConfiguration : BaseConfiguration<Domain.Entities.Auth.RefreshToken>
{
    public override void Configure(EntityTypeBuilder<Domain.Entities.Auth.RefreshToken> builder)
    {
        base.Configure(builder);

        builder.HasOne(x => x.User)
            .WithMany(x => x.RefreshTokens)
            .HasForeignKey(x => x.User_Id);
    }
}
