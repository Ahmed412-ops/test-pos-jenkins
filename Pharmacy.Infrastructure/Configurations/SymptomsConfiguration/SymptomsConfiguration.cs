using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Pharmacy.Infrastructure.Configurations.SymptomsConfiguration;

public class SymptomsConfiguration : BaseConfiguration<Domain.Entities.Symptoms.Symptom>
{
    public override void Configure(EntityTypeBuilder<Domain.Entities.Symptoms.Symptom> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.Name).IsRequired();
    }
}
