using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Entities.Settings;
using Pharmacy.Domain.Enum;

namespace Pharmacy.Infrastructure.Configurations.SystemSettingsConfiguration;

public class SystemSettingsConfiguration : BaseConfiguration<SystemSetting>
{
    public override void Configure(EntityTypeBuilder<SystemSetting> builder)
    {
        base.Configure(builder);

        // Cashback settings configuration
        builder.Property(x => x.Key)
            .IsRequired()
            .HasConversion(
                v => v.ToString(),
                v => Enum.Parse<SettingKeys>(v)
            );

        builder.Property(x => x.Module)
            .IsRequired()
            .HasConversion(
                v => v.ToString(),
                v => Enum.Parse<SettingsModules>(v)
            );

        builder.Property(s => s.Value)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.Type)
            .IsRequired()
            .HasConversion(
                v => v.ToString(),
                v => Enum.Parse<SettingType>(v)
            );

        builder.HasIndex(x => new { x.Module, x.Key })
        .IsUnique();
    }
}
