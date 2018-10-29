using Microsoft.EntityFrameworkCore.Migrations;

namespace inz.Data.Migrations
{
    public partial class Album : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Album",
                table: "Songs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Album",
                table: "Songs");
        }
    }
}
