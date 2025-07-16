namespace Pharmacy.Infrastructure.Configurations.SupplierTransactionConfiguration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Entities.SupplierTransaction;

public class SupplierTransactionConfiguration : BaseConfiguration<SupplierTransaction>
{
    public override void Configure(EntityTypeBuilder<SupplierTransaction> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Amount).IsRequired().HasColumnType("decimal(18,2)");

        builder.Property(x => x.PaymentDate).IsRequired();

        // Optional relationship with SupplierInvoice
        builder
            .HasOne(x => x.SupplierInvoice)
            .WithMany(si => si.SupplierTransactions)
            .HasForeignKey(x => x.SupplierInvoiceId)
            .OnDelete(DeleteBehavior.SetNull);


        builder.HasOne(x => x.ShiftWallet)
            .WithMany(x => x.SupplierTransactions)
            .HasForeignKey(x => x.ShiftWalletId)
            .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);

    }
}
