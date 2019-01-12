using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace inz.Data.Migrations
{
    public partial class fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "releaseSong",
                table: "Song",
                newName: "ReleaseSong");

            migrationBuilder.AddColumn<string>(
                name: "VideoUrl",
                table: "Song",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VideoUrl",
                table: "Song");

            migrationBuilder.RenameColumn(
                name: "ReleaseSong",
                table: "Song",
                newName: "releaseSong");
        }
    }
}
