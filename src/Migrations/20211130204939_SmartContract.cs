using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendToyo.Migrations
{
    public partial class SmartContract : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TypeTokens_Events_EventSku",
                table: "TypeTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypeTokens",
                table: "TypeTokens");

            migrationBuilder.DropIndex(
                name: "IX_TypeTokens_EventSku",
                table: "TypeTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Events",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Sku",
                table: "TypeTokens");

            migrationBuilder.DropColumn(
                name: "EventSku",
                table: "TypeTokens");

            migrationBuilder.DropColumn(
                name: "Sku",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Players",
                newName: "WalletAddress");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "TypeTokens",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "TypeId",
                table: "Tokens",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Events",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypeTokens",
                table: "TypeTokens",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Events",
                table: "Events",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "SmartContractToyoMints",
                columns: table => new
                {
                    TransactionHash = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TokenId = table.Column<int>(type: "int", nullable: false),
                    ChainId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Sender = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WalletAddress = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    TotalSypply = table.Column<int>(type: "int", nullable: false),
                    Gwei = table.Column<int>(type: "int", nullable: false),
                    BlockNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartContractToyoMints", x => new { x.TransactionHash, x.TokenId, x.ChainId });
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SmartContractToyoSyncs",
                columns: table => new
                {
                    ChainId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContractAddress = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EventName = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastBlockNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartContractToyoSyncs", x => new { x.ChainId, x.EventName, x.ContractAddress });
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SmartContractToyoTransfers",
                columns: table => new
                {
                    TransactionHash = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TokenId = table.Column<int>(type: "int", nullable: false),
                    ChainId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BlockNumber = table.Column<int>(type: "int", nullable: false),
                    WalletAddress = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartContractToyoTransfers", x => new { x.TransactionHash, x.TokenId, x.ChainId });
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SmartContractToyoTypes",
                columns: table => new
                {
                    TransactionHash = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    ChainId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BlockNumber = table.Column<int>(type: "int", nullable: false),
                    Sender = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MetaDataUrl = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartContractToyoTypes", x => new { x.TransactionHash, x.TypeId, x.ChainId });
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_TypeId",
                table: "Tokens",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tokens_TypeTokens_TypeId",
                table: "Tokens",
                column: "TypeId",
                principalTable: "TypeTokens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tokens_TypeTokens_TypeId",
                table: "Tokens");

            migrationBuilder.DropTable(
                name: "SmartContractToyoMints");

            migrationBuilder.DropTable(
                name: "SmartContractToyoSyncs");

            migrationBuilder.DropTable(
                name: "SmartContractToyoTransfers");

            migrationBuilder.DropTable(
                name: "SmartContractToyoTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypeTokens",
                table: "TypeTokens");

            migrationBuilder.DropIndex(
                name: "IX_Tokens_TypeId",
                table: "Tokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Events",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TypeTokens");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "WalletAddress",
                table: "Players",
                newName: "Address");

            migrationBuilder.AddColumn<string>(
                name: "Sku",
                table: "TypeTokens",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "EventSku",
                table: "TypeTokens",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<long>(
                name: "TypeId",
                table: "Tokens",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "Sku",
                table: "Events",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypeTokens",
                table: "TypeTokens",
                column: "Sku");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Events",
                table: "Events",
                column: "Sku");

            migrationBuilder.CreateIndex(
                name: "IX_TypeTokens_EventSku",
                table: "TypeTokens",
                column: "EventSku");

            migrationBuilder.AddForeignKey(
                name: "FK_TypeTokens_Events_EventSku",
                table: "TypeTokens",
                column: "EventSku",
                principalTable: "Events",
                principalColumn: "Sku",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
