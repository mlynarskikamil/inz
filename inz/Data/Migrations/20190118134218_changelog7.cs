using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace inz.Data.Migrations
{
    public partial class changelog7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Changelog",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Changelog_ApplicationUserId",
                table: "Changelog",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Changelog_AspNetUsers_ApplicationUserId",
                table: "Changelog",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Changelog_AspNetUsers_ApplicationUserId",
                table: "Changelog");

            migrationBuilder.DropIndex(
                name: "IX_Changelog_ApplicationUserId",
                table: "Changelog");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Changelog");
        }
    }
}
