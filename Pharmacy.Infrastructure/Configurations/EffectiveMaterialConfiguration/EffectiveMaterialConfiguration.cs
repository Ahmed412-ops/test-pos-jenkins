using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Entities.EffectiveMaterial;

namespace Pharmacy.Infrastructure.Configurations.EffectiveMaterialConfiguration;

public class EffectiveMaterialConfiguration : BaseConfiguration<EffectiveMaterial>
{
    public override void Configure(EntityTypeBuilder<EffectiveMaterial> builder)
    {
        base.Configure(builder);
        
        // Basic property configuration
        builder.Property(x => x.Name).IsRequired();
        
        // Relationship: EffectiveMaterial -> EffectiveMaterialCategory (Many-to-One)
        builder.HasOne(x => x.Category)
            .WithMany(c => c.EffectiveMaterials) 
            .HasForeignKey(x => x.CategoryId);
        
        // Relationships using explicit join entities:
        // CommonUses
        builder.HasMany(x => x.CommonUses)
            .WithOne(cu => cu.EffectiveMaterial)
            .HasForeignKey(cu => cu.EffectiveMaterialId);
        
        // OffLabelUses
        builder.HasMany(x => x.OffLabelUses)
            .WithOne(ou => ou.EffectiveMaterial)
            .HasForeignKey(ou => ou.EffectiveMaterialId);
        
        // SideEffects
        builder.HasMany(x => x.SideEffects)
            .WithOne(se => se.EffectiveMaterial)
            .HasForeignKey(se => se.EffectiveMaterialId);
        
        // FoodInteractions
        builder.HasMany(x => x.FoodInteractions)
            .WithOne(fi => fi.EffectiveMaterial)
            .HasForeignKey(fi => fi.EffectiveMaterialId);
        
        // DiseaseInteraction
        builder.HasMany(x => x.DiseaseInteraction)
            .WithOne(di => di.EffectiveMaterial)
            .HasForeignKey(di => di.EffectiveMaterialId);
        
        // DrugInteractions
        builder.HasMany(x => x.MedicinesDrugInteractions)
            .WithOne(di => di.EffectiveMaterial)
            .HasForeignKey(di => di.EffectiveMaterialId);

        // CrossSelling
        builder.HasMany(x => x.MedicinesCrossSelling)
            .WithOne(cs => cs.EffectiveMaterial)
            .HasForeignKey(cs => cs.EffectiveMaterialId);
    }
}
