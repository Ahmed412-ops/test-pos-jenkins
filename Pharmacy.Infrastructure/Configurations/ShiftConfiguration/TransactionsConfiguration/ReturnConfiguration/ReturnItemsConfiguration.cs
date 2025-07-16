using Pharmacy.Domain.Entities.Wallets.Return;

namespace Pharmacy.Infrastructure.Configurations.ShiftConfiguration.TransactionsConfiguration.ReturnConfiguration;

public class ReturnItemsConfiguration : BaseConfiguration<ReturnItem>
{
    public override void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ReturnItem> builder)
    {
        base.Configure(builder);

        builder
            .HasOne(x => x.Return)
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.ReturnId)
            .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);

        builder
            .HasOne(x => x.PrescriptionItem)
            .WithMany()
            .HasForeignKey(x => x.PrescriptionItemId)
            .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
    }
}
