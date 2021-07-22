using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp1.Data.Migrations
{
    public partial class migration5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Release_date",
                table: "Movie",
                newName: "ReleaseDate");

            migrationBuilder.AddColumn<string>(
                name: "OriginalLanguage",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OriginalTitle",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OriginalLanguage",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "OriginalTitle",
                table: "Movie");

            migrationBuilder.RenameColumn(
                name: "ReleaseDate",
                table: "Movie",
                newName: "Release_date");
        }
    }
}
