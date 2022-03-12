using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendToyo.Migrations
{
    public partial class AlterTypeTokenInternal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SmartContractToyoTypes_ChainId",
                table: "SmartContractToyoTypes",
                column: "ChainId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SmartContractToyoTypes_ChainId",
                table: "SmartContractToyoTypes");
        }
    }
}
