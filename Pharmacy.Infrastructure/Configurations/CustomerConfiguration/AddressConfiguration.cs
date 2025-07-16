using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Entities.Customers;

namespace Pharmacy.Infrastructure.Configurations.CustomerConfiguration;

public class AddressConfiguration : BaseConfiguration<Address>
{
    public override void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.Property(a => a.City)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(a => a.StreetName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(a => a.District)
            .HasMaxLength(50);

        builder.Property(a => a.BuildingNumber)
            .HasMaxLength(20);

        builder.Property(a => a.FloorNumber)
            .HasMaxLength(20);

        builder.Property(a => a.ApartmentNumber)
            .HasMaxLength(10);

        builder.Property(a => a.Landmark)
            .HasMaxLength(255);
    }
}