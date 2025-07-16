using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pharmacy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MaterialMedicineRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Barcode",
                table: "MedicationReturnItem",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "MedicineEffectiveMaterialCrossSelling",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EffectiveMaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_By = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Modified_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified_By = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Is_Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineEffectiveMaterialCrossSelling", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicineEffectiveMaterialCrossSelling_EffectiveMaterials_EffectiveMaterialId",
                        column: x => x.EffectiveMaterialId,
                        principalTable: "EffectiveMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicineEffectiveMaterialCrossSelling_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicineEffectiveMaterialInteraction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EffectiveMaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_By = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Modified_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified_By = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Is_Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineEffectiveMaterialInteraction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicineEffectiveMaterialInteraction_EffectiveMaterials_EffectiveMaterialId",
                        column: x => x.EffectiveMaterialId,
                        principalTable: "EffectiveMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicineEffectiveMaterialInteraction_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicineEffectiveMaterialCrossSelling_EffectiveMaterialId",
                table: "MedicineEffectiveMaterialCrossSelling",
                column: "EffectiveMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineEffectiveMaterialCrossSelling_MedicineId",
                table: "MedicineEffectiveMaterialCrossSelling",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineEffectiveMaterialInteraction_EffectiveMaterialId",
                table: "MedicineEffectiveMaterialInteraction",
                column: "EffectiveMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineEffectiveMaterialInteraction_MedicineId",
                table: "MedicineEffectiveMaterialInteraction",
                column: "MedicineId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicineEffectiveMaterialCrossSelling");

            migrationBuilder.DropTable(
                name: "MedicineEffectiveMaterialInteraction");

            migrationBuilder.DropColumn(
                name: "Barcode",
                table: "MedicationReturnItem");
        }
    }
}
