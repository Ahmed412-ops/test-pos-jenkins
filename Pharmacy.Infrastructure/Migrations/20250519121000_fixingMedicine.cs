using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pharmacy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixingMedicine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicineEffectiveMaterial_Medicines_MedicineId",
                table: "MedicineEffectiveMaterial");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicineEffectiveMaterial_Medicines_MedicineId",
                table: "MedicineEffectiveMaterial",
                column: "MedicineId",
                principalTable: "Medicines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicineEffectiveMaterial_Medicines_MedicineId",
                table: "MedicineEffectiveMaterial");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicineEffectiveMaterial_Medicines_MedicineId",
                table: "MedicineEffectiveMaterial",
                column: "MedicineId",
                principalTable: "Medicines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
