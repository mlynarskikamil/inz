using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace inz.Data.Migrations
{
    public partial class addUser3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Song_AspNetUsers_ApplicationUserId",
                table: "Song");

            migrationBuilder.DropIndex(
                name: "IX_Song_ApplicationUserId",
                table: "Song");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Song",
                newName: "ID_User");

            migrationBuilder.AlterColumn<string>(
                name: "ID_User",
                table: "Song",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID_User",
                table: "Song",
                newName: "ApplicationUserId");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Song",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

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
    }
}
