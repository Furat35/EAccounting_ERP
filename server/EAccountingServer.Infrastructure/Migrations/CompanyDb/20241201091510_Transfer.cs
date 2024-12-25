using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EAccountingServer.Infrastructure.Migrations.CompanyDb
{
    /// <inheritdoc />
    public partial class Transfer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BankDetails_BankDetailOppositeId",
                table: "BankDetails");

            migrationBuilder.AddColumn<Guid>(
                name: "BankDetailId",
                table: "CashRegisterDetails",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BankDetailId1",
                table: "CashRegisterDetails",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CashRegisterDetailId",
                table: "BankDetails",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CashRegisterDetails_BankDetailId1",
                table: "CashRegisterDetails",
                column: "BankDetailId1");

            migrationBuilder.CreateIndex(
                name: "IX_BankDetails_BankDetailOppositeId",
                table: "BankDetails",
                column: "BankDetailOppositeId",
                unique: true,
                filter: "[BankDetailOppositeId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_CashRegisterDetails_BankDetails_BankDetailId1",
                table: "CashRegisterDetails",
                column: "BankDetailId1",
                principalTable: "BankDetails",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashRegisterDetails_BankDetails_BankDetailId1",
                table: "CashRegisterDetails");

            migrationBuilder.DropIndex(
                name: "IX_CashRegisterDetails_BankDetailId1",
                table: "CashRegisterDetails");

            migrationBuilder.DropIndex(
                name: "IX_BankDetails_BankDetailOppositeId",
                table: "BankDetails");

            migrationBuilder.DropColumn(
                name: "BankDetailId",
                table: "CashRegisterDetails");

            migrationBuilder.DropColumn(
                name: "BankDetailId1",
                table: "CashRegisterDetails");

            migrationBuilder.DropColumn(
                name: "CashRegisterDetailId",
                table: "BankDetails");

            migrationBuilder.CreateIndex(
                name: "IX_BankDetails_BankDetailOppositeId",
                table: "BankDetails",
                column: "BankDetailOppositeId");
        }
    }
}
