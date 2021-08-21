using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieStore.DB.Migrations
{
    public partial class migration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NameJson",
                table: "MovieLanguage",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameJson",
                table: "MovieLanguage");
        }
    }
}
