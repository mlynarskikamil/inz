using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace inz.Data.Migrations
{
    public partial class logi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Changelog_ID_Changelog_Event",
                table: "Changelog");

            migrationBuilder.CreateIndex(
                name: "IX_Changelog_ID_Changelog_Event",
                table: "Changelog",
                column: "ID_Changelog_Event");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Changelog_ID_Changelog_Event",
                table: "Changelog");

            migrationBuilder.CreateIndex(
                name: "IX_Changelog_ID_Changelog_Event",
                table: "Changelog",
                column: "ID_Changelog_Event",
                unique: true);
        }
    }
}
