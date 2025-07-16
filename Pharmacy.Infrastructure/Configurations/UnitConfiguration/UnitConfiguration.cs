using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Entities.Unit;

namespace Pharmacy.Infrastructure.Configurations.UnitConfiguration;

public class UnitConfiguration : BaseConfiguration<Unit>
{
    public override void Configure(EntityTypeBuilder<Unit> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.Name).IsRequired();
    }
}
