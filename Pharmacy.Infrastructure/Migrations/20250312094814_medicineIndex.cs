using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pharmacy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class medicineIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicationStocks_Suppliers_SupplierId",
                table: "MedicationStocks");

            migrationBuilder.DropIndex(
                name: "IX_MedicationStocks_SupplierId",
                table: "MedicationStocks");

            migrationBuilder.DropColumn(
                name: "IsConfirmed",
                table: "MedicationStocks");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "MedicationStocks");

            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "Medicines",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_Index",
                table: "Medicines",
                column: "Index",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Medicines_Index",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "Index",
                table: "Medicines");

            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmed",
                table: "MedicationStocks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "SupplierId",
                table: "MedicationStocks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicationStocks_SupplierId",
                table: "MedicationStocks",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationStocks_Suppliers_SupplierId",
                table: "MedicationStocks",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
