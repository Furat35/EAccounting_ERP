using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EAccountingServer.Infrastructure.Migrations.CompanyDb
{
    /// <inheritdoc />
    public partial class updatedep : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashRegisterDetails_BankDetails_BankDetailId1",
                table: "CashRegisterDetails");

            migrationBuilder.DropIndex(
                name: "IX_CashRegisterDetails_BankDetailId1",
                table: "CashRegisterDetails");

            migrationBuilder.DropColumn(
                name: "BankDetailId1",
                table: "CashRegisterDetails");

            migrationBuilder.CreateIndex(
                name: "IX_CashRegisterDetails_BankDetailId",
                table: "CashRegisterDetails",
                column: "BankDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_CashRegisterDetails_BankDetails_BankDetailId",
                table: "CashRegisterDetails",
                column: "BankDetailId",
                principalTable: "BankDetails",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashRegisterDetails_BankDetails_BankDetailId",
                table: "CashRegisterDetails");

            migrationBuilder.DropIndex(
                name: "IX_CashRegisterDetails_BankDetailId",
                table: "CashRegisterDetails");

            migrationBuilder.AddColumn<Guid>(
                name: "BankDetailId1",
                table: "CashRegisterDetails",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CashRegisterDetails_BankDetailId1",
                table: "CashRegisterDetails",
                column: "BankDetailId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CashRegisterDetails_BankDetails_BankDetailId1",
                table: "CashRegisterDetails",
                column: "BankDetailId1",
                principalTable: "BankDetails",
                principalColumn: "Id");
        }
    }
}
