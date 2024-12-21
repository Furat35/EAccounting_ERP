using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EAccountingServer.Infrastructure.Migrations.CompanyDb
{
    /// <inheritdoc />
    public partial class addMissing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CashRegisterDetails_BankDetailId",
                table: "CashRegisterDetails");

            migrationBuilder.CreateIndex(
                name: "IX_CashRegisterDetails_BankDetailId",
                table: "CashRegisterDetails",
                column: "BankDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_BankDetails_CashRegisterDetailId",
                table: "BankDetails",
                column: "CashRegisterDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_BankDetails_CashRegisterDetails_CashRegisterDetailId",
                table: "BankDetails",
                column: "CashRegisterDetailId",
                principalTable: "CashRegisterDetails",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankDetails_CashRegisterDetails_CashRegisterDetailId",
                table: "BankDetails");

            migrationBuilder.DropIndex(
                name: "IX_CashRegisterDetails_BankDetailId",
                table: "CashRegisterDetails");

            migrationBuilder.DropIndex(
                name: "IX_BankDetails_CashRegisterDetailId",
                table: "BankDetails");

            migrationBuilder.CreateIndex(
                name: "IX_CashRegisterDetails_BankDetailId",
                table: "CashRegisterDetails",
                column: "BankDetailId",
                unique: true,
                filter: "[BankDetailId] IS NOT NULL");
        }
    }
}
