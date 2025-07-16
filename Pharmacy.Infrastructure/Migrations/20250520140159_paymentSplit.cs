using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pharmacy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class paymentSplit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_ShiftWallets_ShiftWalletId",
                table: "Prescriptions");

            migrationBuilder.RenameColumn(
                name: "ShiftWalletId",
                table: "Prescriptions",
                newName: "ShiftId");

            migrationBuilder.RenameIndex(
                name: "IX_Prescriptions_ShiftWalletId",
                table: "Prescriptions",
                newName: "IX_Prescriptions_ShiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Shifts_ShiftId",
                table: "Prescriptions",
                column: "ShiftId",
                principalTable: "Shifts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Shifts_ShiftId",
                table: "Prescriptions");

            migrationBuilder.RenameColumn(
                name: "ShiftId",
                table: "Prescriptions",
                newName: "ShiftWalletId");

            migrationBuilder.RenameIndex(
                name: "IX_Prescriptions_ShiftId",
                table: "Prescriptions",
                newName: "IX_Prescriptions_ShiftWalletId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_ShiftWallets_ShiftWalletId",
                table: "Prescriptions",
                column: "ShiftWalletId",
                principalTable: "ShiftWallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
