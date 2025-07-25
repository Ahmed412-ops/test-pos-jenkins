﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pharmacy.Infrastructure.Context;

#nullable disable

namespace Pharmacy.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250205095852_permsission")]
    partial class permsission
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Pharmacy.Domain.Entities.Auth.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created_At")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("Created_By")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Expires_On")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Is_Deleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Modified_At")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("Modified_By")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("User_Id")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("Is_Deleted");

                    b.HasIndex("User_Id");

                    b.ToTable("RefreshTokens", (string)null);
                });

            modelBuilder.Entity("Pharmacy.Domain.Entities.Identity.ApplicationRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("ApplicationRoles", (string)null);
                });

            modelBuilder.Entity("Pharmacy.Domain.Entities.Identity.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created_At")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("Created_By")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Date_Of_Birth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("End_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Full_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Is_Active")
                        .HasColumnType("bit");

                    b.Property<bool>("Is_Deleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Last_Login")
                        .HasColumnType("datetime2");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTime?>("Modified_At")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("Modified_By")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Start_Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("PhoneNumber");

                    b.ToTable("ApplicationUsers", (string)null);
                });

            modelBuilder.Entity("Pharmacy.Domain.Entities.Identity.ApplicationUserRole", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("ApplicationUserRoles", (string)null);
                });

            modelBuilder.Entity("Pharmacy.Domain.Entities.Permissions.Permission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created_At")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("Created_By")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Is_Deleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Modified_At")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("Modified_By")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Is_Deleted");

                    b.ToTable("Permissions", (string)null);
                });

            modelBuilder.Entity("Pharmacy.Domain.Entities.Permissions.RolePermission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created_At")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("Created_By")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Is_Deleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Modified_At")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("Modified_By")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PermissionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("Is_Deleted");

                    b.HasIndex("PermissionId");

                    b.HasIndex("RoleId");

                    b.ToTable("RolePermissions", (string)null);
                });

            modelBuilder.Entity("Pharmacy.Domain.Entities.Auth.RefreshToken", b =>
                {
                    b.HasOne("Pharmacy.Domain.Entities.Identity.ApplicationUser", "User")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Pharmacy.Domain.Entities.Identity.ApplicationUserRole", b =>
                {
                    b.HasOne("Pharmacy.Domain.Entities.Identity.ApplicationRole", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pharmacy.Domain.Entities.Identity.ApplicationUser", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Pharmacy.Domain.Entities.Permissions.RolePermission", b =>
                {
                    b.HasOne("Pharmacy.Domain.Entities.Permissions.Permission", "Permission")
                        .WithMany("RolePermissions")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pharmacy.Domain.Entities.Identity.ApplicationRole", "Role")
                        .WithMany("RolePermissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Pharmacy.Domain.Entities.Identity.ApplicationRole", b =>
                {
                    b.Navigation("RolePermissions");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Pharmacy.Domain.Entities.Identity.ApplicationUser", b =>
                {
                    b.Navigation("RefreshTokens");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Pharmacy.Domain.Entities.Permissions.Permission", b =>
                {
                    b.Navigation("RolePermissions");
                });
#pragma warning restore 612, 618
        }
    }
}
