using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace inz.Data.Migrations
{
    public partial class logy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id_User",
                table: "Changelog",
                newName: "ID_User");

            migrationBuilder.AddColumn<int>(
                name: "ID_Song",
                table: "Changelog",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ID_Song",
                table: "Changelog");

            migrationBuilder.RenameColumn(
                name: "ID_User",
                table: "Changelog",
                newName: "Id_User");
        }
    }
}
