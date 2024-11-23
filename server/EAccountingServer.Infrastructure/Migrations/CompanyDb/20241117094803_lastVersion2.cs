using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EAccountingServer.Infrastructure.Migrations.CompanyDb
{
    /// <inheritdoc />
    public partial class lastVersion2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CashRegisterDetails_CashRegisterDetailOppositeId",
                table: "CashRegisterDetails");

            migrationBuilder.DropColumn(
                name: "CashRegisterDetailId",
                table: "CashRegisterDetails");

            migrationBuilder.CreateIndex(
                name: "IX_CashRegisterDetails_CashRegisterDetailOppositeId",
                table: "CashRegisterDetails",
                column: "CashRegisterDetailOppositeId",
                unique: true,
                filter: "[CashRegisterDetailOppositeId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CashRegisterDetails_CashRegisterDetailOppositeId",
                table: "CashRegisterDetails");

            migrationBuilder.AddColumn<Guid>(
                name: "CashRegisterDetailId",
                table: "CashRegisterDetails",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CashRegisterDetails_CashRegisterDetailOppositeId",
                table: "CashRegisterDetails",
                column: "CashRegisterDetailOppositeId");
        }
    }
}
