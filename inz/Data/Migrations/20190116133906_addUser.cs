using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace inz.Data.Migrations
{
    public partial class addUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Song",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ID_User",
                table: "Song",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Song_ApplicationUserId",
                table: "Song",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Song_AspNetUsers_ApplicationUserId",
                table: "Song",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Song_AspNetUsers_ApplicationUserId",
                table: "Song");

            migrationBuilder.DropIndex(
                name: "IX_Song_ApplicationUserId",
                table: "Song");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Song");

            migrationBuilder.DropColumn(
                name: "ID_User",
                table: "Song");
        }
    }
}
