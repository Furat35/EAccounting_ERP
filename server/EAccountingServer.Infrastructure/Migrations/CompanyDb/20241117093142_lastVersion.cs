using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EAccountingServer.Infrastructure.Migrations.CompanyDb
{
    /// <inheritdoc />
    public partial class lastVersion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashRegisterDetails_CashRegisterDetails_CashRegisterDetailId",
                table: "CashRegisterDetails");

            migrationBuilder.DropIndex(
                name: "IX_CashRegisterDetails_CashRegisterDetailId",
                table: "CashRegisterDetails");

            migrationBuilder.AddColumn<Guid>(
                name: "CashRegisterDetailOppositeId",
                table: "CashRegisterDetails",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CashRegisterDetails_CashRegisterDetailOppositeId",
                table: "CashRegisterDetails",
                column: "CashRegisterDetailOppositeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CashRegisterDetails_CashRegisterDetails_CashRegisterDetailOppositeId",
                table: "CashRegisterDetails",
                column: "CashRegisterDetailOppositeId",
                principalTable: "CashRegisterDetails",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashRegisterDetails_CashRegisterDetails_CashRegisterDetailOppositeId",
                table: "CashRegisterDetails");

            migrationBuilder.DropIndex(
                name: "IX_CashRegisterDetails_CashRegisterDetailOppositeId",
                table: "CashRegisterDetails");

            migrationBuilder.DropColumn(
                name: "CashRegisterDetailOppositeId",
                table: "CashRegisterDetails");

            migrationBuilder.CreateIndex(
                name: "IX_CashRegisterDetails_CashRegisterDetailId",
                table: "CashRegisterDetails",
                column: "CashRegisterDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_CashRegisterDetails_CashRegisterDetails_CashRegisterDetailId",
                table: "CashRegisterDetails",
                column: "CashRegisterDetailId",
                principalTable: "CashRegisterDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
