using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pharmacy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixcalcinvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "FinalInvoiceTotal",
                table: "SupplierInvoices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Subtotal",
                table: "SupplierInvoices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalDiscount",
                table: "SupplierInvoices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalTaxAmount",
                table: "SupplierInvoices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "FinalPrice",
                table: "SupplierInvoiceItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SupplierPurchasePrice",
                table: "SupplierInvoiceItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TaxAmount",
                table: "SupplierInvoiceItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPurchasePrice",
                table: "SupplierInvoiceItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalInvoiceTotal",
                table: "SupplierInvoices");

            migrationBuilder.DropColumn(
                name: "Subtotal",
                table: "SupplierInvoices");

            migrationBuilder.DropColumn(
                name: "TotalDiscount",
                table: "SupplierInvoices");

            migrationBuilder.DropColumn(
                name: "TotalTaxAmount",
                table: "SupplierInvoices");

            migrationBuilder.DropColumn(
                name: "FinalPrice",
                table: "SupplierInvoiceItems");

            migrationBuilder.DropColumn(
                name: "SupplierPurchasePrice",
                table: "SupplierInvoiceItems");

            migrationBuilder.DropColumn(
                name: "TaxAmount",
                table: "SupplierInvoiceItems");

            migrationBuilder.DropColumn(
                name: "TotalPurchasePrice",
                table: "SupplierInvoiceItems");
        }
    }
}
