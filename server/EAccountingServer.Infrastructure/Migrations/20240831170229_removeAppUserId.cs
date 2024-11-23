using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EAccountingServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removeAppUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyUser_Users_UserId",
                table: "CompanyUser");

            migrationBuilder.AddColumn<Guid>(
                name: "AppUserId",
                table: "CompanyUser",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyUser_AppUserId",
                table: "CompanyUser",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyUser_Users_AppUserId",
                table: "CompanyUser",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyUser_Users_AppUserId",
                table: "CompanyUser");

            migrationBuilder.DropIndex(
                name: "IX_CompanyUser_AppUserId",
                table: "CompanyUser");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "CompanyUser");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyUser_Users_UserId",
                table: "CompanyUser",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
