using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EAccountingServer.Infrastructure.Migrations.CompanyDb
{
    /// <inheritdoc />
    public partial class addrelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashRegisterDetails_CashRegisterDetails_CashRegisterDetailId",
                table: "CashRegisterDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_CashRegisterDetails_CashRegisterDetails_CashRegisterDetailId",
                table: "CashRegisterDetails",
                column: "CashRegisterDetailId",
                principalTable: "CashRegisterDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashRegisterDetails_CashRegisterDetails_CashRegisterDetailId",
                table: "CashRegisterDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_CashRegisterDetails_CashRegisterDetails_CashRegisterDetailId",
                table: "CashRegisterDetails",
                column: "CashRegisterDetailId",
                principalTable: "CashRegisterDetails",
                principalColumn: "Id");
        }
    }
}
