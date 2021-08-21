using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieStore.DB.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movie",
                columns: table => new
                {
                    MovieId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Runtime = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Synopsis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PosterPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Release_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InitRent = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndRent = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApiId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie", x => x.MovieId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movie");
        }
    }
}
