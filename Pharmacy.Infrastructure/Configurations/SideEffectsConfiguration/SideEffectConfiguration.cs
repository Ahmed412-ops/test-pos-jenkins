using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Entities.SideEffects;

namespace Pharmacy.Infrastructure.Configurations.SideEffectsConfiguration;

public class SideEffectConfiguration : BaseConfiguration<SideEffect>
{
    public override void Configure(EntityTypeBuilder<SideEffect> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.Name).IsRequired();
    }
}
