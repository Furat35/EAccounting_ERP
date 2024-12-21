using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EAccountingServer.Infrastructure.Migrations.CompanyDb
{
    /// <inheritdoc />
    public partial class addMiss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CashRegisterDetails_BankDetailId",
                table: "CashRegisterDetails");

            migrationBuilder.DropIndex(
                name: "IX_BankDetails_BankDetailOppositeId",
                table: "BankDetails");

            migrationBuilder.CreateIndex(
                name: "IX_CashRegisterDetails_BankDetailId",
                table: "CashRegisterDetails",
                column: "BankDetailId",
                unique: true,
                filter: "[BankDetailId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BankDetails_BankDetailOppositeId",
                table: "BankDetails",
                column: "BankDetailOppositeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CashRegisterDetails_BankDetailId",
                table: "CashRegisterDetails");

            migrationBuilder.DropIndex(
                name: "IX_BankDetails_BankDetailOppositeId",
                table: "BankDetails");

            migrationBuilder.CreateIndex(
                name: "IX_CashRegisterDetails_BankDetailId",
                table: "CashRegisterDetails",
                column: "BankDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_BankDetails_BankDetailOppositeId",
                table: "BankDetails",
                column: "BankDetailOppositeId",
                unique: true,
                filter: "[BankDetailOppositeId] IS NOT NULL");
        }
    }
}
