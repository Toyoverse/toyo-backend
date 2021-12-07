using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendToyo.Migrations
{
    public partial class AddSwapAndChangeGweiType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Gwei",
                table: "SmartContractToyoMints",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "SmartContractToyoSwaps",
                columns: table => new
                {
                    TransactionHash = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TokenId = table.Column<int>(type: "int", nullable: false),
                    ChainId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FromTypeId = table.Column<int>(type: "int", nullable: false),
                    ToTypeId = table.Column<int>(type: "int", nullable: false),
                    Sender = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BlockNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartContractToyoSwaps", x => new { x.TransactionHash, x.TokenId, x.ChainId });
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SmartContractToyoSwaps");

            migrationBuilder.AlterColumn<int>(
                name: "Gwei",
                table: "SmartContractToyoMints",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
