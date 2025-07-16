using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Entities.Food;

namespace Pharmacy.Infrastructure.Configurations.FoodConfiguration;

public class FoodConfigruation : BaseConfiguration<Food>
{
    public override void Configure(EntityTypeBuilder<Food> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.Name).IsRequired();
    }
}
