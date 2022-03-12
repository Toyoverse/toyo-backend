using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendToyo.Migrations
{
    public partial class ToyosAndPartsForPlayer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Toyo",
                table: "Toyo");

            migrationBuilder.RenameTable(
                name: "Toyo",
                newName: "Toyos");

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
                        .Annotation("MySql:CharSet", "utf8mb4")
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartsPlayer");

            migrationBuilder.DropTable(
                name: "ToyosPlayer");

            migrationBuilder.DropTable(
                name: "Stats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Toyos",
                table: "Toyos");

            migrationBuilder.RenameTable(
                name: "Toyos",
                newName: "Toyo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Toyo",
                table: "Toyo",
                column: "Id");
        }
    }
}
