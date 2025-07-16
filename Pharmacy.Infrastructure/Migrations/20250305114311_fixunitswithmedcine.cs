using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pharmacy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixunitswithmedcine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicines_Units_DefaultSellingUnitId",
                table: "Medicines");

            migrationBuilder.DropIndex(
                name: "IX_Medicines_DefaultSellingUnitId",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "CalcUnit",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "DefaultSellingUnitId",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "QuantityForCalcUnit",
                table: "Medicines");

            migrationBuilder.AddColumn<bool>(
                name: "CalcUnit",
                table: "MedicineUnits",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanBeSold",
                table: "MedicineUnits",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "MedicineUnits",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "QuantityForCalcUnit",
                table: "MedicineUnits",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CalcUnit",
                table: "MedicineUnits");

            migrationBuilder.DropColumn(
                name: "CanBeSold",
                table: "MedicineUnits");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "MedicineUnits");

            migrationBuilder.DropColumn(
                name: "QuantityForCalcUnit",
                table: "MedicineUnits");

            migrationBuilder.AddColumn<bool>(
                name: "CalcUnit",
                table: "Medicines",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "DefaultSellingUnitId",
                table: "Medicines",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<decimal>(
                name: "QuantityForCalcUnit",
                table: "Medicines",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_DefaultSellingUnitId",
                table: "Medicines",
                column: "DefaultSellingUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicines_Units_DefaultSellingUnitId",
                table: "Medicines",
                column: "DefaultSellingUnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
