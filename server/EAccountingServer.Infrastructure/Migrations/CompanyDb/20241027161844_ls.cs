using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EAccountingServer.Infrastructure.Migrations.CompanyDb
{
    /// <inheritdoc />
    public partial class ls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CashRegisterDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CashRegisterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepositAmount = table.Column<decimal>(type: "money", nullable: false),
                    WithdrawalAmount = table.Column<decimal>(type: "money", nullable: false),
                    CashRegisterDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CashRegisterDetailOppositeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashRegisterDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CashRegisterDetails_CashRegisterDetails_CashRegisterDetailOppositeId",
                        column: x => x.CashRegisterDetailOppositeId,
                        principalTable: "CashRegisterDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CashRegisterDetails_CashRegisters_CashRegisterId",
                        column: x => x.CashRegisterId,
                        principalTable: "CashRegisters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CashRegisterDetails_CashRegisterDetailOppositeId",
                table: "CashRegisterDetails",
                column: "CashRegisterDetailOppositeId");

            migrationBuilder.CreateIndex(
                name: "IX_CashRegisterDetails_CashRegisterId",
                table: "CashRegisterDetails",
                column: "CashRegisterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CashRegisterDetails");
        }
    }
}
