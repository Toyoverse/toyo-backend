using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendToyo.Migrations
{
    public partial class SwapToyoIdAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SmartContractToyoSwaps",
                table: "SmartContractToyoSwaps");

            migrationBuilder.RenameColumn(
                name: "TokenId",
                table: "SmartContractToyoSwaps",
                newName: "ToTokenId");

            migrationBuilder.AddColumn<int>(
                name: "FromTokenId",
                table: "SmartContractToyoSwaps",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SmartContractToyoSwaps",
                table: "SmartContractToyoSwaps",
                columns: new[] { "TransactionHash", "FromTokenId", "ToTokenId", "ChainId" });

            migrationBuilder.CreateTable(
                name: "Parts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TorsoId = table.Column<int>(type: "int", nullable: false),
                    Part = table.Column<int>(type: "int", nullable: false),
                    RetroBone = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    Variants = table.Column<int>(type: "int", nullable: false),
                    Colors = table.Column<int>(type: "int", nullable: false),
                    Cyber = table.Column<int>(type: "int", nullable: false),
                    Prefix = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Desc = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parts", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Toyo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Existe = table.Column<int>(type: "int", nullable: false),
                    Material = table.Column<int>(type: "int", nullable: false),
                    BodyType = table.Column<int>(type: "int", nullable: false),
                    Rarity = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    Variants = table.Column<int>(type: "int", nullable: false),
                    Colors = table.Column<int>(type: "int", nullable: false),
                    Cyber = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Desc = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Toyo", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parts");

            migrationBuilder.DropTable(
                name: "Toyo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SmartContractToyoSwaps",
                table: "SmartContractToyoSwaps");

            migrationBuilder.DropColumn(
                name: "FromTokenId",
                table: "SmartContractToyoSwaps");

            migrationBuilder.RenameColumn(
                name: "ToTokenId",
                table: "SmartContractToyoSwaps",
                newName: "TokenId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SmartContractToyoSwaps",
                table: "SmartContractToyoSwaps",
                columns: new[] { "TransactionHash", "TokenId", "ChainId" });
        }
    }
}
