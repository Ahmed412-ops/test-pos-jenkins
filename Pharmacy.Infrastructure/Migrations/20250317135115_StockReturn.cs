using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pharmacy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class StockReturn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MedicationReturn",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReturnReferenceNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnStatus = table.Column<int>(type: "int", nullable: false),
                    SupplierInvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_By = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Modified_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified_By = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Is_Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicationReturn", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicationReturn_SupplierInvoices_SupplierInvoiceId",
                        column: x => x.SupplierInvoiceId,
                        principalTable: "SupplierInvoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_MedicationReturn_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MedicationReturnItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicationReturnId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicineUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuantityToReturn = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ExpiryDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ReturnReason = table.Column<int>(type: "int", nullable: false),
                    AdditionalReasonDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReturnValue = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_By = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Modified_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified_By = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Is_Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicationReturnItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicationReturnItem_MedicationReturn_MedicationReturnId",
                        column: x => x.MedicationReturnId,
                        principalTable: "MedicationReturn",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicationReturnItem_MedicineUnits_MedicineUnitId",
                        column: x => x.MedicineUnitId,
                        principalTable: "MedicineUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicationReturn_SupplierId",
                table: "MedicationReturn",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationReturn_SupplierInvoiceId",
                table: "MedicationReturn",
                column: "SupplierInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationReturnItem_MedicationReturnId",
                table: "MedicationReturnItem",
                column: "MedicationReturnId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationReturnItem_MedicineUnitId",
                table: "MedicationReturnItem",
                column: "MedicineUnitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicationReturnItem");

            migrationBuilder.DropTable(
                name: "MedicationReturn");
        }
    }
}
