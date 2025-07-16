namespace Pharmacy.Infrastructure.Configurations.ShiftConfiguration;

public class ShiftConfiguration : BaseConfiguration<Domain.Entities.Wallets.Shift>
{
    public override void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.Entities.Wallets.Shift> builder)
    {
        base.Configure(builder);
        builder.HasOne(x => x.OpenedBy)
            .WithMany(x => x.Shifts)
            .HasForeignKey(x => x.OpenedById)
            .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
    }
}
