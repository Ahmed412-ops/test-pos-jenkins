using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Entities.DosageForm;

namespace Pharmacy.Infrastructure.Configurations.DosageFormConfiguration;

public class DosageFormConfiguration : BaseConfiguration<DosageForm>
{
    public override void Configure(EntityTypeBuilder<DosageForm> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.Name).IsRequired();
    }
}
