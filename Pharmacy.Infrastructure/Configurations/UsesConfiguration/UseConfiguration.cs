using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Entities.Uses;

namespace Pharmacy.Infrastructure.Configurations.UsesConfiguration;

public class UseConfiguration : BaseConfiguration<Use>
{
    public override void Configure(EntityTypeBuilder<Use> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.Name).IsRequired();
    }
}
