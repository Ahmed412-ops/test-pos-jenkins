using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pharmacy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EnableContactOption = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_By = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Modified_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified_By = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Is_Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StreetName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    District = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BuildingNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    FloorNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ApartmentNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Landmark = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_By = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Modified_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified_By = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Is_Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerChronicDisease",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiseaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_By = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Modified_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified_By = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Is_Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerChronicDisease", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerChronicDisease_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerChronicDisease_Diseases_DiseaseId",
                        column: x => x.DiseaseId,
                        principalTable: "Diseases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerChronicMedicine",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_By = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Modified_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified_By = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Is_Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerChronicMedicine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerChronicMedicine_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerChronicMedicine_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhoneNumbers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsWhatsApp = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_By = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Modified_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified_By = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Is_Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumbers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhoneNumbers_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_CustomerId",
                table: "Address",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerChronicDisease_CustomerId",
                table: "CustomerChronicDisease",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerChronicDisease_DiseaseId",
                table: "CustomerChronicDisease",
                column: "DiseaseId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerChronicMedicine_CustomerId",
                table: "CustomerChronicMedicine",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerChronicMedicine_MedicineId",
                table: "CustomerChronicMedicine",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Is_Deleted",
                table: "Customers",
                column: "Is_Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumbers_CustomerId",
                table: "PhoneNumbers",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumbers_Is_Deleted",
                table: "PhoneNumbers",
                column: "Is_Deleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "CustomerChronicDisease");

            migrationBuilder.DropTable(
                name: "CustomerChronicMedicine");

            migrationBuilder.DropTable(
                name: "PhoneNumbers");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
