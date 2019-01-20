using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace inz.Data.Migrations
{
    public partial class changelog6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Changelog_ID_Changelog_Event",
                table: "Changelog",
                column: "ID_Changelog_Event",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Changelog_Changelog_Name_List_ID_Changelog_Event",
                table: "Changelog",
                column: "ID_Changelog_Event",
                principalTable: "Changelog_Name_List",
                principalColumn: "ID_Changelog_Event",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Changelog_Changelog_Name_List_ID_Changelog_Event",
                table: "Changelog");

            migrationBuilder.DropIndex(
                name: "IX_Changelog_ID_Changelog_Event",
                table: "Changelog");
        }
    }
}
