using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EAccountingServer.Infrastructure.Migrations.CompanyDb
{
    /// <inheritdoc />
    public partial class addBankDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BankId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepositAmount = table.Column<decimal>(type: "money", nullable: false),
                    WithdrawalAmount = table.Column<decimal>(type: "money", nullable: false),
                    BankDetailOppositeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankDetails_BankDetails_BankDetailOppositeId",
                        column: x => x.BankDetailOppositeId,
                        principalTable: "BankDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BankDetails_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankDetails_BankDetailOppositeId",
                table: "BankDetails",
                column: "BankDetailOppositeId");

            migrationBuilder.CreateIndex(
                name: "IX_BankDetails_BankId",
                table: "BankDetails",
                column: "BankId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankDetails");
        }
    }
}
