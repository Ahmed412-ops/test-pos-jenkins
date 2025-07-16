using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Entities.Manufacturers;

namespace Pharmacy.Infrastructure.Configurations.ManufacturerConfiguration;

public class ManufacturerConfiguration : BaseConfiguration<Manufacturer>
{
    public override void Configure(EntityTypeBuilder<Manufacturer> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.Name).IsRequired();
    }
}
