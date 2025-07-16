using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Entities.Permissions;

namespace Pharmacy.Infrastructure.Configurations.PermissionsConfiguration;

public class PermissionConfiguration : BaseConfiguration<Permission>
{
    public override void Configure(EntityTypeBuilder<Permission> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.Title).IsRequired();
        builder.HasMany(x => x.RolePermissions).WithOne(x => x.Permission).HasForeignKey(x => x.PermissionId);
    } 
}
