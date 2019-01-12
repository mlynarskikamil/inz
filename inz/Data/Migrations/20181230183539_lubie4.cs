using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace inz.Data.Migrations
{
    public partial class lubie4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Like",
                table: "Song",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Unlike",
                table: "Song",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Opinion_ID_Song",
                table: "Opinion");

            migrationBuilder.DropColumn(
                name: "Like",
                table: "Song");

            migrationBuilder.DropColumn(
                name: "Unlike",
                table: "Song");

            migrationBuilder.CreateIndex(
                name: "IX_Opinion_ID_Song",
                table: "Opinion",
                column: "ID_Song");
        }
    }
}
