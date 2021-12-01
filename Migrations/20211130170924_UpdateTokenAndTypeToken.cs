using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendToyo.Migrations
{
    public partial class UpdateTokenAndTypeToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tokens_TypeTokens_TypeSku",
                table: "Tokens");

            migrationBuilder.DropIndex(
                name: "IX_Tokens_TypeSku",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "TypeSku",
                table: "Tokens");

            migrationBuilder.AddColumn<long>(
                name: "TypeId",
                table: "Tokens",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Tokens");

            migrationBuilder.AddColumn<string>(
                name: "TypeSku",
                table: "Tokens",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_TypeSku",
                table: "Tokens",
                column: "TypeSku");

            migrationBuilder.AddForeignKey(
                name: "FK_Tokens_TypeTokens_TypeSku",
                table: "Tokens",
                column: "TypeSku",
                principalTable: "TypeTokens",
                principalColumn: "Sku",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
