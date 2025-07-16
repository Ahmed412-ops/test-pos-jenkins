using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Entities.EffectiveMaterial;

namespace Pharmacy.Infrastructure.Configurations.EffectiveMaterialConfiguration;

public class EffectiveMaterialCategoryConfiguration : BaseConfiguration<EffectiveMaterialCategory>
{
    public override void Configure(EntityTypeBuilder<EffectiveMaterialCategory> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.Name).IsRequired();
    }
}
