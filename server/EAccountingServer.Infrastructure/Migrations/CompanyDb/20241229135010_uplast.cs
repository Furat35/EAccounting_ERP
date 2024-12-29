﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EAccountingServer.Infrastructure.Migrations.CompanyDb
{
    /// <inheritdoc />
    public partial class uplast : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "ProductDetail",
                type: "money",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "ProductDetail");
        }
    }
}
