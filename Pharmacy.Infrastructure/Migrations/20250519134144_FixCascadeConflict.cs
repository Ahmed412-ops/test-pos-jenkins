using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pharmacy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixCascadeConflict : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderItems_MedicineUnits_MedicineUnitId",
                table: "PurchaseOrderItems");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderItems_MedicineUnits_MedicineUnitId",
                table: "PurchaseOrderItems",
                column: "MedicineUnitId",
                principalTable: "MedicineUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderItems_MedicineUnits_MedicineUnitId",
                table: "PurchaseOrderItems");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderItems_MedicineUnits_MedicineUnitId",
                table: "PurchaseOrderItems",
                column: "MedicineUnitId",
                principalTable: "MedicineUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
