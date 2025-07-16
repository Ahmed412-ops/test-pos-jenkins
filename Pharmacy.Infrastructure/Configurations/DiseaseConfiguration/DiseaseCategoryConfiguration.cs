using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Entities.Disease;

namespace Pharmacy.Infrastructure.Configurations.DiseaseConfiguration;

public class DiseaseCategoryConfiguration : BaseConfiguration<DiseaseCategory>
{
    public override void Configure(EntityTypeBuilder<DiseaseCategory> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.Name).IsRequired();
    }
}
