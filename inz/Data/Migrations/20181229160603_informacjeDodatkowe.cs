using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace inz.Data.Migrations
{
    public partial class informacjeDodatkowe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Birthday",
                table: "Artist",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Artist",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "Artist",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReleaseDate",
                table: "Album",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "Artist");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Artist");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "Artist");

            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "Album");
        }
    }
}
