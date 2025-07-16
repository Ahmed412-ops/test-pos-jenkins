using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pharmacy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCrossSellingAndDrugInteraction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EffectiveMaterialCrossSelling_EffectiveMaterials_CrossSellingMaterialId",
                table: "EffectiveMaterialCrossSelling");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectiveMaterialCrossSelling_EffectiveMaterials_EffectiveMaterialId",
                table: "EffectiveMaterialCrossSelling");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectiveMaterialDrugInteraction_EffectiveMaterials_EffectiveMaterialId",
                table: "EffectiveMaterialDrugInteraction");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectiveMaterialDrugInteraction_EffectiveMaterials_InteractingMaterialId",
                table: "EffectiveMaterialDrugInteraction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EffectiveMaterialDrugInteraction",
                table: "EffectiveMaterialDrugInteraction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EffectiveMaterialCrossSelling",
                table: "EffectiveMaterialCrossSelling");

            migrationBuilder.RenameTable(
                name: "EffectiveMaterialDrugInteraction",
                newName: "EffectiveMaterialDrugInteractions");

            migrationBuilder.RenameTable(
                name: "EffectiveMaterialCrossSelling",
                newName: "EffectiveMaterialCrossSellings");

            migrationBuilder.RenameIndex(
                name: "IX_EffectiveMaterialDrugInteraction_InteractingMaterialId",
                table: "EffectiveMaterialDrugInteractions",
                newName: "IX_EffectiveMaterialDrugInteractions_InteractingMaterialId");

            migrationBuilder.RenameIndex(
                name: "IX_EffectiveMaterialDrugInteraction_EffectiveMaterialId",
                table: "EffectiveMaterialDrugInteractions",
                newName: "IX_EffectiveMaterialDrugInteractions_EffectiveMaterialId");

            migrationBuilder.RenameIndex(
                name: "IX_EffectiveMaterialCrossSelling_EffectiveMaterialId",
                table: "EffectiveMaterialCrossSellings",
                newName: "IX_EffectiveMaterialCrossSellings_EffectiveMaterialId");

            migrationBuilder.RenameIndex(
                name: "IX_EffectiveMaterialCrossSelling_CrossSellingMaterialId",
                table: "EffectiveMaterialCrossSellings",
                newName: "IX_EffectiveMaterialCrossSellings_CrossSellingMaterialId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EffectiveMaterialDrugInteractions",
                table: "EffectiveMaterialDrugInteractions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EffectiveMaterialCrossSellings",
                table: "EffectiveMaterialCrossSellings",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_EffectiveMaterialDrugInteractions_Is_Deleted",
                table: "EffectiveMaterialDrugInteractions",
                column: "Is_Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_EffectiveMaterialCrossSellings_Is_Deleted",
                table: "EffectiveMaterialCrossSellings",
                column: "Is_Deleted");

            migrationBuilder.AddForeignKey(
                name: "FK_EffectiveMaterialCrossSellings_EffectiveMaterials_CrossSellingMaterialId",
                table: "EffectiveMaterialCrossSellings",
                column: "CrossSellingMaterialId",
                principalTable: "EffectiveMaterials",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EffectiveMaterialCrossSellings_EffectiveMaterials_EffectiveMaterialId",
                table: "EffectiveMaterialCrossSellings",
                column: "EffectiveMaterialId",
                principalTable: "EffectiveMaterials",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EffectiveMaterialDrugInteractions_EffectiveMaterials_EffectiveMaterialId",
                table: "EffectiveMaterialDrugInteractions",
                column: "EffectiveMaterialId",
                principalTable: "EffectiveMaterials",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EffectiveMaterialDrugInteractions_EffectiveMaterials_InteractingMaterialId",
                table: "EffectiveMaterialDrugInteractions",
                column: "InteractingMaterialId",
                principalTable: "EffectiveMaterials",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EffectiveMaterialCrossSellings_EffectiveMaterials_CrossSellingMaterialId",
                table: "EffectiveMaterialCrossSellings");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectiveMaterialCrossSellings_EffectiveMaterials_EffectiveMaterialId",
                table: "EffectiveMaterialCrossSellings");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectiveMaterialDrugInteractions_EffectiveMaterials_EffectiveMaterialId",
                table: "EffectiveMaterialDrugInteractions");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectiveMaterialDrugInteractions_EffectiveMaterials_InteractingMaterialId",
                table: "EffectiveMaterialDrugInteractions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EffectiveMaterialDrugInteractions",
                table: "EffectiveMaterialDrugInteractions");

            migrationBuilder.DropIndex(
                name: "IX_EffectiveMaterialDrugInteractions_Is_Deleted",
                table: "EffectiveMaterialDrugInteractions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EffectiveMaterialCrossSellings",
                table: "EffectiveMaterialCrossSellings");

            migrationBuilder.DropIndex(
                name: "IX_EffectiveMaterialCrossSellings_Is_Deleted",
                table: "EffectiveMaterialCrossSellings");

            migrationBuilder.RenameTable(
                name: "EffectiveMaterialDrugInteractions",
                newName: "EffectiveMaterialDrugInteraction");

            migrationBuilder.RenameTable(
                name: "EffectiveMaterialCrossSellings",
                newName: "EffectiveMaterialCrossSelling");

            migrationBuilder.RenameIndex(
                name: "IX_EffectiveMaterialDrugInteractions_InteractingMaterialId",
                table: "EffectiveMaterialDrugInteraction",
                newName: "IX_EffectiveMaterialDrugInteraction_InteractingMaterialId");

            migrationBuilder.RenameIndex(
                name: "IX_EffectiveMaterialDrugInteractions_EffectiveMaterialId",
                table: "EffectiveMaterialDrugInteraction",
                newName: "IX_EffectiveMaterialDrugInteraction_EffectiveMaterialId");

            migrationBuilder.RenameIndex(
                name: "IX_EffectiveMaterialCrossSellings_EffectiveMaterialId",
                table: "EffectiveMaterialCrossSelling",
                newName: "IX_EffectiveMaterialCrossSelling_EffectiveMaterialId");

            migrationBuilder.RenameIndex(
                name: "IX_EffectiveMaterialCrossSellings_CrossSellingMaterialId",
                table: "EffectiveMaterialCrossSelling",
                newName: "IX_EffectiveMaterialCrossSelling_CrossSellingMaterialId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EffectiveMaterialDrugInteraction",
                table: "EffectiveMaterialDrugInteraction",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EffectiveMaterialCrossSelling",
                table: "EffectiveMaterialCrossSelling",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EffectiveMaterialCrossSelling_EffectiveMaterials_CrossSellingMaterialId",
                table: "EffectiveMaterialCrossSelling",
                column: "CrossSellingMaterialId",
                principalTable: "EffectiveMaterials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EffectiveMaterialCrossSelling_EffectiveMaterials_EffectiveMaterialId",
                table: "EffectiveMaterialCrossSelling",
                column: "EffectiveMaterialId",
                principalTable: "EffectiveMaterials",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EffectiveMaterialDrugInteraction_EffectiveMaterials_EffectiveMaterialId",
                table: "EffectiveMaterialDrugInteraction",
                column: "EffectiveMaterialId",
                principalTable: "EffectiveMaterials",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EffectiveMaterialDrugInteraction_EffectiveMaterials_InteractingMaterialId",
                table: "EffectiveMaterialDrugInteraction",
                column: "InteractingMaterialId",
                principalTable: "EffectiveMaterials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
