using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Entities.Disease;

namespace Pharmacy.Infrastructure.Configurations.DiseaseConfiguration;

public class DiseaseConfiguration : BaseConfiguration<Disease>
{
    public override void Configure(EntityTypeBuilder<Disease> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.Name).IsRequired();
       
        builder.HasMany(x => x.Symptoms)
            .WithOne(x => x.Disease)
            .HasForeignKey(x => x.DiseaseId); 
        
        builder.HasOne(x => x.DiseaseCategory)
            .WithMany(x => x.Diseases)
            .HasForeignKey(x => x.DiseaseCategoryId);
    }
}
