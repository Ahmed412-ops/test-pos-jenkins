using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pharmacy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class recorderpointsettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecorderPointSettingss",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PreferredSupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReorderPoint = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RestockingQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NotificationsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_By = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Modified_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified_By = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Is_Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecorderPointSettingss", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecorderPointSettingss_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecorderPointSettingss_Suppliers_PreferredSupplierId",
                        column: x => x.PreferredSupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecorderPointSettingss_Is_Deleted",
                table: "RecorderPointSettingss",
                column: "Is_Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_RecorderPointSettingss_MedicineId",
                table: "RecorderPointSettingss",
                column: "MedicineId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecorderPointSettingss_PreferredSupplierId",
                table: "RecorderPointSettingss",
                column: "PreferredSupplierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecorderPointSettingss");
        }
    }
}
