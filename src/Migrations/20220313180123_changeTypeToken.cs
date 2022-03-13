using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendToyo.Migrations
{
    public partial class changeTypeToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tokens_TypeTokens_TypeId",
                table: "Tokens");

            migrationBuilder.DropForeignKey(
                name: "FK_TypeTokens_SmartContractToyoTypes_ChainId",
                table: "TypeTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_TypeTokens_SmartContractToyoTypes_TypeId",
                table: "TypeTokens");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_TypeTokens_TypeId",
                table: "TypeTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypeTokens",
                table: "TypeTokens");

            migrationBuilder.RenameTable(
                name: "TypeTokens",
                newName: "TypeToken");

            migrationBuilder.RenameIndex(
                name: "IX_TypeTokens_ChainId",
                table: "TypeToken",
                newName: "IX_TypeToken_ChainId");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "TypeToken",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TypeToken",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "ChainId",
                table: "TypeToken",
                type: "varchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_TypeToken_TypeId",
                table: "TypeToken",
                column: "TypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypeToken",
                table: "TypeToken",
                columns: new[] { "TypeId", "ChainId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Tokens_TypeToken_TypeId",
                table: "Tokens",
                column: "TypeId",
                principalTable: "TypeToken",
                principalColumn: "TypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TypeToken_SmartContractToyoTypes_ChainId",
                table: "TypeToken",
                column: "ChainId",
                principalTable: "SmartContractToyoTypes",
                principalColumn: "ChainId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TypeToken_SmartContractToyoTypes_TypeId",
                table: "TypeToken",
                column: "TypeId",
                principalTable: "SmartContractToyoTypes",
                principalColumn: "TypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tokens_TypeToken_TypeId",
                table: "Tokens");

            migrationBuilder.DropForeignKey(
                name: "FK_TypeToken_SmartContractToyoTypes_ChainId",
                table: "TypeToken");

            migrationBuilder.DropForeignKey(
                name: "FK_TypeToken_SmartContractToyoTypes_TypeId",
                table: "TypeToken");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_TypeToken_TypeId",
                table: "TypeToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypeToken",
                table: "TypeToken");

            migrationBuilder.RenameTable(
                name: "TypeToken",
                newName: "TypeTokens");

            migrationBuilder.RenameIndex(
                name: "IX_TypeToken_ChainId",
                table: "TypeTokens",
                newName: "IX_TypeTokens_ChainId");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "TypeTokens",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TypeTokens",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "ChainId",
                table: "TypeTokens",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_TypeTokens_TypeId",
                table: "TypeTokens",
                column: "TypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypeTokens",
                table: "TypeTokens",
                columns: new[] { "TypeId", "ChainId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Tokens_TypeTokens_TypeId",
                table: "Tokens",
                column: "TypeId",
                principalTable: "TypeTokens",
                principalColumn: "TypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TypeTokens_SmartContractToyoTypes_ChainId",
                table: "TypeTokens",
                column: "ChainId",
                principalTable: "SmartContractToyoTypes",
                principalColumn: "ChainId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TypeTokens_SmartContractToyoTypes_TypeId",
                table: "TypeTokens",
                column: "TypeId",
                principalTable: "SmartContractToyoTypes",
                principalColumn: "TypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
