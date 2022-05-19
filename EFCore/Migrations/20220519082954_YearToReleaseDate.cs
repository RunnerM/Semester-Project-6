using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore.Migrations
{
    public partial class YearToReleaseDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Year",
                table: "Movie");

            migrationBuilder.AddColumn<string>(
                name: "ReleaseDate",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "Movie");

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Movie",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
