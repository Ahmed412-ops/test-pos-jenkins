using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pharmacy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixCategoryIdinEM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EffectiveMaterials_EffectiveMaterialCategorys_EffectiveMaterialCategoryId",
                table: "EffectiveMaterials");

            migrationBuilder.RenameColumn(
                name: "EffectiveMaterialCategoryId",
                table: "EffectiveMaterials",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_EffectiveMaterials_EffectiveMaterialCategoryId",
                table: "EffectiveMaterials",
                newName: "IX_EffectiveMaterials_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_EffectiveMaterials_EffectiveMaterialCategorys_CategoryId",
                table: "EffectiveMaterials",
                column: "CategoryId",
                principalTable: "EffectiveMaterialCategorys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EffectiveMaterials_EffectiveMaterialCategorys_CategoryId",
                table: "EffectiveMaterials");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "EffectiveMaterials",
                newName: "EffectiveMaterialCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_EffectiveMaterials_CategoryId",
                table: "EffectiveMaterials",
                newName: "IX_EffectiveMaterials_EffectiveMaterialCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_EffectiveMaterials_EffectiveMaterialCategorys_EffectiveMaterialCategoryId",
                table: "EffectiveMaterials",
                column: "EffectiveMaterialCategoryId",
                principalTable: "EffectiveMaterialCategorys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
