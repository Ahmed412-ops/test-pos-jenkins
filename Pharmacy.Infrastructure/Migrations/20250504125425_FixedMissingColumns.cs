using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pharmacy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixedMissingColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShiftSaless");

            migrationBuilder.AddColumn<Guid>(
                name: "ShiftWalletId",
                table: "SupplierTransactions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WalletId",
                table: "SupplierTransactions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRecevied",
                table: "SupplierInvoices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "DosagePerKgForChildren",
                table: "Medicines",
                type: "decimal(4,2)",
                precision: 4,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsedForChildren",
                table: "Medicines",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Is_Liquid",
                table: "DosageForms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceNumber = table.Column<int>(type: "int", maxLength: 50, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShiftWalletId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AmountPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_By = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Modified_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified_By = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Is_Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Prescriptions_ShiftWallets_ShiftWalletId",
                        column: x => x.ShiftWalletId,
                        principalTable: "ShiftWallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrescriptionItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrescriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicationStockId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicineUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AppliedDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_By = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Modified_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified_By = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Is_Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescriptionItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrescriptionItems_MedicationStocks_MedicationStockId",
                        column: x => x.MedicationStockId,
                        principalTable: "MedicationStocks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PrescriptionItems_MedicineUnits_MedicineUnitId",
                        column: x => x.MedicineUnitId,
                        principalTable: "MedicineUnits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PrescriptionItems_Prescriptions_PrescriptionId",
                        column: x => x.PrescriptionId,
                        principalTable: "Prescriptions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SupplierTransactions_ShiftWalletId",
                table: "SupplierTransactions",
                column: "ShiftWalletId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierTransactions_WalletId",
                table: "SupplierTransactions",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionItems_Is_Deleted",
                table: "PrescriptionItems",
                column: "Is_Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionItems_MedicationStockId",
                table: "PrescriptionItems",
                column: "MedicationStockId");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionItems_MedicineUnitId",
                table: "PrescriptionItems",
                column: "MedicineUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionItems_PrescriptionId",
                table: "PrescriptionItems",
                column: "PrescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_CustomerId",
                table: "Prescriptions",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_Is_Deleted",
                table: "Prescriptions",
                column: "Is_Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_ShiftWalletId",
                table: "Prescriptions",
                column: "ShiftWalletId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierTransactions_ShiftWallets_ShiftWalletId",
                table: "SupplierTransactions",
                column: "ShiftWalletId",
                principalTable: "ShiftWallets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierTransactions_Wallets_WalletId",
                table: "SupplierTransactions",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupplierTransactions_ShiftWallets_ShiftWalletId",
                table: "SupplierTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierTransactions_Wallets_WalletId",
                table: "SupplierTransactions");

            migrationBuilder.DropTable(
                name: "PrescriptionItems");

            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_SupplierTransactions_ShiftWalletId",
                table: "SupplierTransactions");

            migrationBuilder.DropIndex(
                name: "IX_SupplierTransactions_WalletId",
                table: "SupplierTransactions");

            migrationBuilder.DropColumn(
                name: "ShiftWalletId",
                table: "SupplierTransactions");

            migrationBuilder.DropColumn(
                name: "WalletId",
                table: "SupplierTransactions");

            migrationBuilder.DropColumn(
                name: "IsRecevied",
                table: "SupplierInvoices");

            migrationBuilder.DropColumn(
                name: "DosagePerKgForChildren",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "IsUsedForChildren",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "Is_Liquid",
                table: "DosageForms");

            migrationBuilder.CreateTable(
                name: "ShiftSaless",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MedicationStockId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShiftWalletId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_By = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Is_Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Modified_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified_By = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaleDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftSaless", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShiftSaless_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ShiftSaless_MedicationStocks_MedicationStockId",
                        column: x => x.MedicationStockId,
                        principalTable: "MedicationStocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShiftSaless_ShiftWallets_ShiftWalletId",
                        column: x => x.ShiftWalletId,
                        principalTable: "ShiftWallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShiftSaless_CustomerId",
                table: "ShiftSaless",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftSaless_Is_Deleted",
                table: "ShiftSaless",
                column: "Is_Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftSaless_MedicationStockId",
                table: "ShiftSaless",
                column: "MedicationStockId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftSaless_ShiftWalletId",
                table: "ShiftSaless",
                column: "ShiftWalletId");
        }
    }
}
