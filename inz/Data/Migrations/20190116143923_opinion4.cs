using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace inz.Data.Migrations
{
    public partial class opinion4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Opinion_AspNetUsers_ApplicationUserId",
                table: "Opinion");

            migrationBuilder.DropIndex(
                name: "IX_Opinion_ApplicationUserId",
                table: "Opinion");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Opinion");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Opinion",
                newName: "Id_User");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id_User",
                table: "Opinion",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Opinion",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Opinion_ApplicationUserId",
                table: "Opinion",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Opinion_AspNetUsers_ApplicationUserId",
                table: "Opinion",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
