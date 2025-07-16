using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Entities.EffectiveMaterial;

namespace Pharmacy.Infrastructure.Configurations.EffectiveMaterialConfiguration;

public class EffectiveMaterialDrugInteractionConfiguration : BaseConfiguration<EffectiveMaterialDrugInteraction>
{
    public override void Configure(EntityTypeBuilder<EffectiveMaterialDrugInteraction> builder)
    {
        base.Configure(builder);

        builder.HasOne(x => x.EffectiveMaterial)
            .WithMany(x => x.EM_DrugInteractions)
            .HasForeignKey(x => x.EffectiveMaterialId)
            .OnDelete(DeleteBehavior.NoAction);
            
        builder.HasOne(x => x.InteractingMaterial)
            .WithMany(x => x.DI_EffectiveMaterials)
            .HasForeignKey(x => x.InteractingMaterialId)
            .OnDelete(DeleteBehavior.NoAction);
    }    
}
