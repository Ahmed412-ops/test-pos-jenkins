using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pharmacy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PrescriptionTransactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AmountPaid",
                table: "Prescriptions",
                newName: "CreditUsed");

            migrationBuilder.CreateTable(
                name: "BalanceTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrescriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_By = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Modified_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified_By = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Is_Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BalanceTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BalanceTransactions_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BalanceTransactions_Prescriptions_PrescriptionId",
                        column: x => x.PrescriptionId,
                        principalTable: "Prescriptions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PrescriptionTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceNumber = table.Column<int>(type: "int", maxLength: 50, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrescriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShiftWalletId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AmountPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_By = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Modified_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified_By = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Is_Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescriptionTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrescriptionTransactions_Prescriptions_PrescriptionId",
                        column: x => x.PrescriptionId,
                        principalTable: "Prescriptions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PrescriptionTransactions_ShiftWallets_ShiftWalletId",
                        column: x => x.ShiftWalletId,
                        principalTable: "ShiftWallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BalanceTransactions_CustomerId",
                table: "BalanceTransactions",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_BalanceTransactions_Is_Deleted",
                table: "BalanceTransactions",
                column: "Is_Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_BalanceTransactions_PrescriptionId",
                table: "BalanceTransactions",
                column: "PrescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionTransactions_Is_Deleted",
                table: "PrescriptionTransactions",
                column: "Is_Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionTransactions_PrescriptionId",
                table: "PrescriptionTransactions",
                column: "PrescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionTransactions_ShiftWalletId",
                table: "PrescriptionTransactions",
                column: "ShiftWalletId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BalanceTransactions");

            migrationBuilder.DropTable(
                name: "PrescriptionTransactions");

            migrationBuilder.RenameColumn(
                name: "CreditUsed",
                table: "Prescriptions",
                newName: "AmountPaid");
        }
    }
}
