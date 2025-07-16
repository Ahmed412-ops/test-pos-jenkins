using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pharmacy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class medicine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Medicines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Barcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManufacturerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicineCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DosageFormId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Strength = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StorageConditions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefaultSellingUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CalcUnit = table.Column<bool>(type: "bit", nullable: false),
                    QuantityForCalcUnit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_By = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Modified_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified_By = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Is_Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medicines_DosageForms_DosageFormId",
                        column: x => x.DosageFormId,
                        principalTable: "DosageForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Medicines_Manufacturers_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Medicines_MedicineCategorys_MedicineCategoryId",
                        column: x => x.MedicineCategoryId,
                        principalTable: "MedicineCategorys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Medicines_Units_DefaultSellingUnitId",
                        column: x => x.DefaultSellingUnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MedicineCrossSellings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CrossSellingMedicineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_By = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Modified_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified_By = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Is_Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineCrossSellings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicineCrossSellings_Medicines_CrossSellingMedicineId",
                        column: x => x.CrossSellingMedicineId,
                        principalTable: "Medicines",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MedicineCrossSellings_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicines",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MedicineEffectiveMaterial",
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
                    table.PrimaryKey("PK_MedicineEffectiveMaterial", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicineEffectiveMaterial_EffectiveMaterials_EffectiveMaterialId",
                        column: x => x.EffectiveMaterialId,
                        principalTable: "EffectiveMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicineEffectiveMaterial_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MedicineUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_By = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Modified_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified_By = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Is_Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicineUnits_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicineUnits_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicineCrossSellings_CrossSellingMedicineId",
                table: "MedicineCrossSellings",
                column: "CrossSellingMedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineCrossSellings_Is_Deleted",
                table: "MedicineCrossSellings",
                column: "Is_Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineCrossSellings_MedicineId",
                table: "MedicineCrossSellings",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineEffectiveMaterial_EffectiveMaterialId",
                table: "MedicineEffectiveMaterial",
                column: "EffectiveMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineEffectiveMaterial_MedicineId",
                table: "MedicineEffectiveMaterial",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_DefaultSellingUnitId",
                table: "Medicines",
                column: "DefaultSellingUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_DosageFormId",
                table: "Medicines",
                column: "DosageFormId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_Is_Deleted",
                table: "Medicines",
                column: "Is_Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_ManufacturerId",
                table: "Medicines",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_MedicineCategoryId",
                table: "Medicines",
                column: "MedicineCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineUnits_Is_Deleted",
                table: "MedicineUnits",
                column: "Is_Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineUnits_MedicineId",
                table: "MedicineUnits",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineUnits_UnitId",
                table: "MedicineUnits",
                column: "UnitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicineCrossSellings");

            migrationBuilder.DropTable(
                name: "MedicineEffectiveMaterial");

            migrationBuilder.DropTable(
                name: "MedicineUnits");

            migrationBuilder.DropTable(
                name: "Medicines");
        }
    }
}
