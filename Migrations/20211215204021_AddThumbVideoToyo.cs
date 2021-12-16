using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendToyo.Migrations
{
    public partial class AddThumbVideoToyo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Thumb",
                table: "Toyo",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Video",
                table: "Toyo",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Thumb",
                table: "Toyo");

            migrationBuilder.DropColumn(
                name: "Video",
                table: "Toyo");
        }
    }
}
