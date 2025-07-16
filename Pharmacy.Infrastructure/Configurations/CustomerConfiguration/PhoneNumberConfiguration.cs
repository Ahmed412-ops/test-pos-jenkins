using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Entities.Customers;

namespace Pharmacy.Infrastructure.Configurations.CustomerConfiguration;

public class PhoneNumberConfiguration : BaseConfiguration<PhoneNumber>
{
    public override void Configure(EntityTypeBuilder<PhoneNumber> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Number)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(p => p.IsWhatsApp)
            .HasDefaultValue(false);
    }
}
