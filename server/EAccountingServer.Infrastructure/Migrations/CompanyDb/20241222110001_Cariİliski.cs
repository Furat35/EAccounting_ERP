﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EAccountingServer.Infrastructure.Migrations.CompanyDb
{
    /// <inheritdoc />
    public partial class Cariİliski : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BankDetailId",
                table: "CustomerDetails",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerDetailId",
                table: "BankDetails",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankDetailId",
                table: "CustomerDetails");

            migrationBuilder.DropColumn(
                name: "CustomerDetailId",
                table: "BankDetails");
        }
    }
}
