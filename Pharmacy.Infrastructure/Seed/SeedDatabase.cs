using System.Drawing.Drawing2D;
using System.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Constants.Permissions;
using Pharmacy.Domain;
using Pharmacy.Domain.Entities.Identity;
using Pharmacy.Domain.Entities.Permissions;
using Pharmacy.Domain.Entities.Settings;
using Pharmacy.Domain.Enum;
using Pharmacy.Infrastructure.Context;
namespace Pharmacy.Infrastructure.Seed;

public class SeedDatabase
{
    public static async Task Seed(IUnitOfWork<AppDbContext> unitOfWork, AppDbContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        await SeedRoleData(roleManager);
        await SeedUserData(context, userManager);
        await SeedPermissionsData(context, roleManager);
        await SeedSetting(context);
        await unitOfWork.SaveChangesAsync();
    }

    private static async Task SeedUserData(AppDbContext context, UserManager<ApplicationUser> userManager)
    {
        Guid.TryParse(ApplicationConstants.Programmer, out var programmer);
        Guid.TryParse(ApplicationConstants.AdminId, out var admin);

        var user = new ApplicationUser
        {
            Id = programmer,
            Full_Name = "Super Admin",
            Email = "Programmer@programmer.com",
            NormalizedEmail = "PROGRAMMER@PROGRAMMER.COM",
            UserName = "Programmer",
            NormalizedUserName = "PROGRAMMER",
            Is_Active = true,
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString("D"),
        };

        var Admin = new ApplicationUser
        {
            Id = admin,
            Full_Name = "Admin",
            Email = "Admin@Admin.com",
            NormalizedEmail = "ADMIN@ADMIN.COM",
            UserName = "Admin",
            NormalizedUserName = "ADMIN",
            Is_Active = true,
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString("D"),
        };

        var set = context.Set<ApplicationUser>();
        if (!await set.AnyAsync(a => a.Id == programmer || a.NormalizedUserName == user.NormalizedUserName))
        {
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHasher.HashPassword(user, "P00000");
            await set.AddAsync(user);

            await context.SaveChangesAsync();
            await userManager.AddToRoleAsync(user, nameof(UserRole.SuperAdmin));
        }
        if (!await set.AnyAsync(a => a.Id == admin || a.NormalizedUserName == Admin.NormalizedUserName))
        {
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            Admin.PasswordHash = passwordHasher.HashPassword(Admin, "P00000");
            await set.AddAsync(Admin);

            await context.SaveChangesAsync();
            await userManager.AddToRoleAsync(Admin, nameof(UserRole.SuperAdmin));
        }
    }

    private static async Task SeedRoleData(RoleManager<ApplicationRole> roleManager)
    {
        var roles = new List<ApplicationRole>();

        foreach (UserRole role in Enum.GetValues(typeof(UserRole)))
            roles.Add(new ApplicationRole { Name = role.ToString() });

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role.Name!))
                await roleManager.CreateAsync(role);
        }
    }
    private static async Task SeedPermissionsData(AppDbContext context, RoleManager<ApplicationRole> roleManager)
    {
        var permissionSet = context.Set<Permission>();
        var permissionList = await permissionSet.ToListAsync();

        foreach (var feature in Enum.GetValues(typeof(PermissionConstant.FeaturesEnum)))
        {
            foreach (var action in Enum.GetValues(typeof(PermissionConstant.BaseActions)))
            {
                var permission = new Permission
                {
                    Id = Guid.NewGuid(),
                    Title = $"{feature}.{action}"
                };

                if (!await permissionSet.AnyAsync(p => p.Title == permission.Title))
                {
                    await permissionSet.AddAsync(permission);
                    permissionList.Add(permission);
                }
            }
        }

        // Assign permissions to roles (e.g., Admin gets all)
        var superAdmin = await roleManager.FindByNameAsync(nameof(UserRole.SuperAdmin));
        if (superAdmin != null)
        {
            foreach (var permission in permissionList)
            {
                var rolePermission = new RolePermission
                {
                    RoleId = superAdmin.Id,
                    PermissionId = permission.Id
                };

                var rolePermissionSet = context.Set<RolePermission>();
                if (!await rolePermissionSet.AnyAsync(rp => rp.RoleId == superAdmin.Id && rp.PermissionId == permission.Id))
                {
                    await rolePermissionSet.AddAsync(rolePermission);
                }
            }
        }


        await context.SaveChangesAsync();
    }
    private static async Task SeedSetting(AppDbContext context)
    {
        var settingsSet = context.Set<SystemSetting>();

        // Check if any settings already exist
        if (!await settingsSet.AnyAsync())
        {
            var settings = new List<SystemSetting>
            {
                new SystemSetting
                {
                    Module = SettingsModules.Cashback,
                    Key = SettingKeys.Enabled,
                    Value = "true",
                    Type = SettingType.Boolean
                },
                new SystemSetting
                {
                    Module = SettingsModules.Cashback,
                    Key = SettingKeys.MaxLimit,
                    Value = "100.00",
                    Type = SettingType.Decimal
                },
                new SystemSetting
                {
                    Module = SettingsModules.Cashback,
                    Key = SettingKeys.MinLimit,
                    Value = "10.00",
                    Type = SettingType.Decimal
                },
                new SystemSetting
                {
                    Module = SettingsModules.Cashback,
                    Key = SettingKeys.CashbackPercentage,
                    Value = "2.00",
                    Type = SettingType.Decimal
                },
                new SystemSetting
                {
                    Module = SettingsModules.Cashback,
                    Key = SettingKeys.MinApplicableAmount,
                    Value = "400.00",
                    Type = SettingType.Decimal
                },
                new SystemSetting
                {
                    Module = SettingsModules.CreditPolicy,
                    Key = SettingKeys.DefaultMaxDebt,
                    Value = "1000.00",
                    Type = SettingType.Decimal
                },
                new SystemSetting
                {
                    Module = SettingsModules.CreditPolicy,
                    Key = SettingKeys.DefaultGracePeriodDays,
                    Value = "30",
                    Type = SettingType.Integer
                },
                new SystemSetting
                {
                    Module = SettingsModules.CreditPolicy,
                    Key = SettingKeys.DebtWarningLimit,
                    Value = "90.00",
                    Type = SettingType.Decimal
                },
                new SystemSetting
                {
                    Module = SettingsModules.Ads,
                    Key = SettingKeys.InvoiceFooterAds,
                    Value = "The medicine will reach you wherever you are",
                    Type = SettingType.String
                }
            };

            await settingsSet.AddRangeAsync(settings);
            await context.SaveChangesAsync();
        }
    }
}

