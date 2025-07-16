using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Entities.EffectiveMaterial;

namespace Pharmacy.Infrastructure.Configurations.EffectiveMaterialConfiguration;

public class EffectiveMaterialCrossSellingConfiguration : BaseConfiguration<EffectiveMaterialCrossSelling>
{
    public override void Configure(EntityTypeBuilder<EffectiveMaterialCrossSelling> builder)
    {
        base.Configure(builder);

        builder.HasOne(x => x.EffectiveMaterial)
            .WithMany(x => x.EM_CrossSelling)
            .HasForeignKey(x => x.EffectiveMaterialId)
            .OnDelete(DeleteBehavior.NoAction);
            
        builder.HasOne(x => x.CrossSellingMaterial)
            .WithMany(x => x.CS_EffectiveMaterials)
            .HasForeignKey(x => x.CrossSellingMaterialId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
