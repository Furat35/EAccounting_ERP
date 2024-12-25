﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EAccountingServer.Infrastructure.Migrations.CompanyDb
{
    /// <inheritdoc />
    public partial class Cariİliski2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CashRegisterDetailId",
                table: "CustomerDetails",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerDetailId",
                table: "CashRegisterDetails",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CashRegisterDetailId",
                table: "CustomerDetails");

            migrationBuilder.DropColumn(
                name: "CustomerDetailId",
                table: "CashRegisterDetails");
        }
    }
}
