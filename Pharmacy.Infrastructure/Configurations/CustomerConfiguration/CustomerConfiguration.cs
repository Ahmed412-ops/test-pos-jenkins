using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Entities.Customers;

namespace Pharmacy.Infrastructure.Configurations.CustomerConfiguration
{
    public class CustomerConfiguration : BaseConfiguration<Customer>
    {
        public override void Configure(EntityTypeBuilder<Customer> builder)
        {
            base.Configure(builder);

            builder.Property(c => c.Name).IsRequired().HasMaxLength(50);

            builder.Property(c => c.EnableContactOption).HasDefaultValue(false);

            builder
                .HasMany(c => c.Addresses)
                .WithOne(a => a.Customer)
                .HasForeignKey(a => a.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(c => c.PhoneNumbers)
                .WithOne(p => p.Customer)
                .HasForeignKey(p => p.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(x => x.CustomerChronicMedicines)
                .WithOne(x => x.Customer)
                .HasForeignKey(x => x.CustomerId);

            builder
                .HasMany(x => x.CustomerChronicDiseases)
                .WithOne(x => x.Customer)
                .HasForeignKey(x => x.CustomerId);
        }
    }
}
