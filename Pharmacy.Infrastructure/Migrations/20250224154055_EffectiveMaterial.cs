using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pharmacy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EffectiveMaterial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EffectiveMaterials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientInformation_En = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientInformation_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BlackBoxWarning = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EffectiveMaterialCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_EffectiveMaterials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EffectiveMaterials_EffectiveMaterialCategorys_EffectiveMaterialCategoryId",
                        column: x => x.EffectiveMaterialCategoryId,
                        principalTable: "EffectiveMaterialCategorys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Uses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_Uses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EffectiveMaterialCrossSelling",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EffectiveMaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CrossSellingMaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_By = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Modified_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified_By = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Is_Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EffectiveMaterialCrossSelling", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EffectiveMaterialCrossSelling_EffectiveMaterials_CrossSellingMaterialId",
                        column: x => x.CrossSellingMaterialId,
                        principalTable: "EffectiveMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EffectiveMaterialCrossSelling_EffectiveMaterials_EffectiveMaterialId",
                        column: x => x.EffectiveMaterialId,
                        principalTable: "EffectiveMaterials",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EffectiveMaterialDisease",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EffectiveMaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiseaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_By = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Modified_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified_By = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Is_Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EffectiveMaterialDisease", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EffectiveMaterialDisease_Diseases_DiseaseId",
                        column: x => x.DiseaseId,
                        principalTable: "Diseases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EffectiveMaterialDisease_EffectiveMaterials_EffectiveMaterialId",
                        column: x => x.EffectiveMaterialId,
                        principalTable: "EffectiveMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EffectiveMaterialDrugInteraction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EffectiveMaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InteractingMaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_By = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Modified_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified_By = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Is_Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EffectiveMaterialDrugInteraction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EffectiveMaterialDrugInteraction_EffectiveMaterials_EffectiveMaterialId",
                        column: x => x.EffectiveMaterialId,
                        principalTable: "EffectiveMaterials",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EffectiveMaterialDrugInteraction_EffectiveMaterials_InteractingMaterialId",
                        column: x => x.InteractingMaterialId,
                        principalTable: "EffectiveMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EffectiveMaterialFood",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EffectiveMaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FoodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_By = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Modified_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified_By = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Is_Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EffectiveMaterialFood", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EffectiveMaterialFood_EffectiveMaterials_EffectiveMaterialId",
                        column: x => x.EffectiveMaterialId,
                        principalTable: "EffectiveMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EffectiveMaterialFood_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EffectiveMaterialSideEffect",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EffectiveMaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SideEffectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_By = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Modified_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified_By = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Is_Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EffectiveMaterialSideEffect", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EffectiveMaterialSideEffect_EffectiveMaterials_EffectiveMaterialId",
                        column: x => x.EffectiveMaterialId,
                        principalTable: "EffectiveMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EffectiveMaterialSideEffect_SideEffects_SideEffectId",
                        column: x => x.SideEffectId,
                        principalTable: "SideEffects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EffectiveMaterialCommonUse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EffectiveMaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_By = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Modified_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified_By = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Is_Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EffectiveMaterialCommonUse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EffectiveMaterialCommonUse_EffectiveMaterials_EffectiveMaterialId",
                        column: x => x.EffectiveMaterialId,
                        principalTable: "EffectiveMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EffectiveMaterialCommonUse_Uses_UseId",
                        column: x => x.UseId,
                        principalTable: "Uses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EffectiveMaterialOffLabelUse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EffectiveMaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_By = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Modified_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified_By = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Is_Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EffectiveMaterialOffLabelUse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EffectiveMaterialOffLabelUse_EffectiveMaterials_EffectiveMaterialId",
                        column: x => x.EffectiveMaterialId,
                        principalTable: "EffectiveMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EffectiveMaterialOffLabelUse_Uses_UseId",
                        column: x => x.UseId,
                        principalTable: "Uses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EffectiveMaterialCommonUse_EffectiveMaterialId",
                table: "EffectiveMaterialCommonUse",
                column: "EffectiveMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectiveMaterialCommonUse_UseId",
                table: "EffectiveMaterialCommonUse",
                column: "UseId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectiveMaterialCrossSelling_CrossSellingMaterialId",
                table: "EffectiveMaterialCrossSelling",
                column: "CrossSellingMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectiveMaterialCrossSelling_EffectiveMaterialId",
                table: "EffectiveMaterialCrossSelling",
                column: "EffectiveMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectiveMaterialDisease_DiseaseId",
                table: "EffectiveMaterialDisease",
                column: "DiseaseId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectiveMaterialDisease_EffectiveMaterialId",
                table: "EffectiveMaterialDisease",
                column: "EffectiveMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectiveMaterialDrugInteraction_EffectiveMaterialId",
                table: "EffectiveMaterialDrugInteraction",
                column: "EffectiveMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectiveMaterialDrugInteraction_InteractingMaterialId",
                table: "EffectiveMaterialDrugInteraction",
                column: "InteractingMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectiveMaterialFood_EffectiveMaterialId",
                table: "EffectiveMaterialFood",
                column: "EffectiveMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectiveMaterialFood_FoodId",
                table: "EffectiveMaterialFood",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectiveMaterialOffLabelUse_EffectiveMaterialId",
                table: "EffectiveMaterialOffLabelUse",
                column: "EffectiveMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectiveMaterialOffLabelUse_UseId",
                table: "EffectiveMaterialOffLabelUse",
                column: "UseId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectiveMaterials_EffectiveMaterialCategoryId",
                table: "EffectiveMaterials",
                column: "EffectiveMaterialCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectiveMaterials_Is_Deleted",
                table: "EffectiveMaterials",
                column: "Is_Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_EffectiveMaterialSideEffect_EffectiveMaterialId",
                table: "EffectiveMaterialSideEffect",
                column: "EffectiveMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectiveMaterialSideEffect_SideEffectId",
                table: "EffectiveMaterialSideEffect",
                column: "SideEffectId");

            migrationBuilder.CreateIndex(
                name: "IX_Uses_Is_Deleted",
                table: "Uses",
                column: "Is_Deleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EffectiveMaterialCommonUse");

            migrationBuilder.DropTable(
                name: "EffectiveMaterialCrossSelling");

            migrationBuilder.DropTable(
                name: "EffectiveMaterialDisease");

            migrationBuilder.DropTable(
                name: "EffectiveMaterialDrugInteraction");

            migrationBuilder.DropTable(
                name: "EffectiveMaterialFood");

            migrationBuilder.DropTable(
                name: "EffectiveMaterialOffLabelUse");

            migrationBuilder.DropTable(
                name: "EffectiveMaterialSideEffect");

            migrationBuilder.DropTable(
                name: "Uses");

            migrationBuilder.DropTable(
                name: "EffectiveMaterials");
        }
    }
}
