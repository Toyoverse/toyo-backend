using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendToyo.Migrations
{
    public partial class AlterTokenType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tokens_TypeTokens_TypeId",
                table: "Tokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypeTokens",
                table: "TypeTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Toyo",
                table: "Toyo");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TypeTokens");

            migrationBuilder.DropColumn(
                name: "MetaDataUrl",
                table: "TypeTokens");

            migrationBuilder.RenameTable(
                name: "TypeTokens",
                newName: "TypeToken");

            migrationBuilder.RenameTable(
                name: "Toyo",
                newName: "Toyos");

            migrationBuilder.AlterColumn<int>(
                name: "TypeId",
                table: "Tokens",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "TypeId",
                table: "TypeToken",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

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

            migrationBuilder.AddColumn<string>(
                name: "ChainId",
                table: "TypeToken",
                type: "varchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "TypeToken",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "Toyos",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Thumb",
                table: "Toyos",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Video",
                table: "Toyos",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_SmartContractToyoTypes_ChainId",
                table: "SmartContractToyoTypes",
                column: "ChainId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_SmartContractToyoTypes_TypeId",
                table: "SmartContractToyoTypes",
                column: "TypeId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_TypeToken_TypeId",
                table: "TypeToken",
                column: "TypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypeToken",
                table: "TypeToken",
                columns: new[] { "TypeId", "ChainId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Toyos",
                table: "Toyos",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Stats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stats", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ToyosPlayer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ToyoId = table.Column<int>(type: "int", nullable: false),
                    TokenId = table.Column<int>(type: "int", nullable: false),
                    Vitality = table.Column<int>(type: "int", nullable: false),
                    Strength = table.Column<int>(type: "int", nullable: false),
                    Resistance = table.Column<int>(type: "int", nullable: false),
                    CyberForce = table.Column<int>(type: "int", nullable: false),
                    Resilience = table.Column<int>(type: "int", nullable: false),
                    Precision = table.Column<int>(type: "int", nullable: false),
                    Technique = table.Column<int>(type: "int", nullable: false),
                    Analysis = table.Column<int>(type: "int", nullable: false),
                    Speed = table.Column<int>(type: "int", nullable: false),
                    Agility = table.Column<int>(type: "int", nullable: false),
                    Stamina = table.Column<int>(type: "int", nullable: false),
                    Luck = table.Column<int>(type: "int", nullable: false),
                    WalletAddress = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ChainId = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ChangeValue = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToyosPlayer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ToyosPlayer_Toyos_ToyoId",
                        column: x => x.ToyoId,
                        principalTable: "Toyos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PartsPlayer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PartId = table.Column<int>(type: "int", nullable: false),
                    StatId = table.Column<int>(type: "int", nullable: false),
                    BonusStat = table.Column<int>(type: "int", nullable: false),
                    TokenId = table.Column<int>(type: "int", nullable: false),
                    WalletAddress = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ChainId = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartsPlayer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartsPlayer_Parts_PartId",
                        column: x => x.PartId,
                        principalTable: "Parts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PartsPlayer_Stats_StatId",
                        column: x => x.StatId,
                        principalTable: "Stats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Stats",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, null, "Vitality" },
                    { 2, null, "Strength" },
                    { 3, null, "Resistance" },
                    { 4, null, "CyberForce" },
                    { 5, null, "Resilience" },
                    { 6, null, "Precision" },
                    { 7, null, "Technique" },
                    { 8, null, "Analysis" },
                    { 9, null, "Speed" },
                    { 10, null, "Agility" },
                    { 11, null, "Stamina" },
                    { 12, null, "Luck" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SmartContractToyoTypes_ChainId",
                table: "SmartContractToyoTypes",
                column: "ChainId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeToken_ChainId",
                table: "TypeToken",
                column: "ChainId");

            migrationBuilder.CreateIndex(
                name: "IX_PartsPlayer_PartId",
                table: "PartsPlayer",
                column: "PartId");

            migrationBuilder.CreateIndex(
                name: "IX_PartsPlayer_StatId",
                table: "PartsPlayer",
                column: "StatId");

            migrationBuilder.CreateIndex(
                name: "IX_ToyosPlayer_ToyoId",
                table: "ToyosPlayer",
                column: "ToyoId");

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

            migrationBuilder.DropTable(
                name: "PartsPlayer");

            migrationBuilder.DropTable(
                name: "ToyosPlayer");

            migrationBuilder.DropTable(
                name: "Stats");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_SmartContractToyoTypes_ChainId",
                table: "SmartContractToyoTypes");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_SmartContractToyoTypes_TypeId",
                table: "SmartContractToyoTypes");

            migrationBuilder.DropIndex(
                name: "IX_SmartContractToyoTypes_ChainId",
                table: "SmartContractToyoTypes");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_TypeToken_TypeId",
                table: "TypeToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypeToken",
                table: "TypeToken");

            migrationBuilder.DropIndex(
                name: "IX_TypeToken_ChainId",
                table: "TypeToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Toyos",
                table: "Toyos");

            migrationBuilder.DropColumn(
                name: "ChainId",
                table: "TypeToken");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "TypeToken");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "Toyos");

            migrationBuilder.DropColumn(
                name: "Thumb",
                table: "Toyos");

            migrationBuilder.DropColumn(
                name: "Video",
                table: "Toyos");

            migrationBuilder.RenameTable(
                name: "TypeToken",
                newName: "TypeTokens");

            migrationBuilder.RenameTable(
                name: "Toyos",
                newName: "Toyo");

            migrationBuilder.AlterColumn<Guid>(
                name: "TypeId",
                table: "Tokens",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AlterColumn<long>(
                name: "TypeId",
                table: "TypeTokens",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "TypeTokens",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "MetaDataUrl",
                table: "TypeTokens",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypeTokens",
                table: "TypeTokens",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Toyo",
                table: "Toyo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tokens_TypeTokens_TypeId",
                table: "Tokens",
                column: "TypeId",
                principalTable: "TypeTokens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
