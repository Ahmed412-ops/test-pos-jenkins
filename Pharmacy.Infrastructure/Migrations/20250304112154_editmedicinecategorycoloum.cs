using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pharmacy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class editmedicinecategorycoloum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicines_MedicineCategorys_MedicineCategoryId",
                table: "Medicines");

            migrationBuilder.RenameColumn(
                name: "MedicineCategoryId",
                table: "Medicines",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Medicines_MedicineCategoryId",
                table: "Medicines",
                newName: "IX_Medicines_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicines_MedicineCategorys_CategoryId",
                table: "Medicines",
                column: "CategoryId",
                principalTable: "MedicineCategorys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicines_MedicineCategorys_CategoryId",
                table: "Medicines");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Medicines",
                newName: "MedicineCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Medicines_CategoryId",
                table: "Medicines",
                newName: "IX_Medicines_MedicineCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicines_MedicineCategorys_MedicineCategoryId",
                table: "Medicines",
                column: "MedicineCategoryId",
                principalTable: "MedicineCategorys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
