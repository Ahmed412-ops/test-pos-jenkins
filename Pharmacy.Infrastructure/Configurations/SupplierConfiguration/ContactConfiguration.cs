using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Entities.Supplier;

namespace Pharmacy.Infrastructure.Configurations.SupplierConfiguration;

public class ContactConfiguration : BaseConfiguration<Contact>
{
    public override void Configure(EntityTypeBuilder<Contact> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.PhoneNumber).IsRequired();
        builder.Property(x => x.Name).IsRequired();
    }
}
