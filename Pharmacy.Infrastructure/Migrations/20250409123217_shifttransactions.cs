using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pharmacy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class shifttransactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClosingBalance",
                table: "ShiftWallets");

            migrationBuilder.DropColumn(
                name: "TotalExpenses",
                table: "ShiftWallets");

            migrationBuilder.DropColumn(
                name: "TotalSales",
                table: "ShiftWallets");

            migrationBuilder.RenameColumn(
                name: "ActualClosingAmount",
                table: "ShiftWallets",
                newName: "ActualClosingBalance");

            migrationBuilder.CreateTable(
                name: "ExpenseCategorys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_ExpenseCategorys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShiftSaless",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicationStockId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShiftWalletId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SaleDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_By = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Modified_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified_By = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Is_Deleted = table.Column<bool>(type: "bit", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "CashExpenses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShiftWalletId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExpenseDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaidTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_By = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Modified_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified_By = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Is_Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashExpenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CashExpenses_ExpenseCategorys_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ExpenseCategorys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CashExpenses_ShiftWallets_ShiftWalletId",
                        column: x => x.ShiftWalletId,
                        principalTable: "ShiftWallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CashExpenses_CategoryId",
                table: "CashExpenses",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CashExpenses_Is_Deleted",
                table: "CashExpenses",
                column: "Is_Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_CashExpenses_ShiftWalletId",
                table: "CashExpenses",
                column: "ShiftWalletId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseCategorys_Is_Deleted",
                table: "ExpenseCategorys",
                column: "Is_Deleted");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CashExpenses");

            migrationBuilder.DropTable(
                name: "ShiftSaless");

            migrationBuilder.DropTable(
                name: "ExpenseCategorys");

            migrationBuilder.RenameColumn(
                name: "ActualClosingBalance",
                table: "ShiftWallets",
                newName: "ActualClosingAmount");

            migrationBuilder.AddColumn<decimal>(
                name: "ClosingBalance",
                table: "ShiftWallets",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalExpenses",
                table: "ShiftWallets",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalSales",
                table: "ShiftWallets",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
