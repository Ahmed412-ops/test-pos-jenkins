using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Entities.Wallets.Expense;

namespace Pharmacy.Infrastructure.Configurations.ShiftConfiguration.TransactionsConfiguration.ExpenseConfiguration;

public class ExpensesConfiguration : BaseConfiguration<CashExpense>
{
    public override void Configure(EntityTypeBuilder<CashExpense> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.Amount).IsRequired();
        builder.Property(x => x.ExpenseDateTime).IsRequired();
        builder.Property(x => x.PaidTo).IsRequired(false);
        builder.Property(x => x.Notes).IsRequired(false);

        builder
            .HasOne(x => x.Category)
            .WithMany(x => x.CashExpenses)
            .HasForeignKey(x => x.CategoryId);

        builder
            .HasOne(x => x.ShiftWallet)
            .WithMany(x => x.CashExpenses)
            .HasForeignKey(x => x.ShiftWalletId);
    }
}
