using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace inz.Data.Migrations
{
    public partial class lubie2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Opinion_OpinionID_Opinion",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Song_Opinion_OpinionID_Opinion",
                table: "Song");

            migrationBuilder.DropIndex(
                name: "IX_Song_OpinionID_Opinion",
                table: "Song");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_OpinionID_Opinion",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "OpinionID_Opinion",
                table: "Song");

            migrationBuilder.DropColumn(
                name: "OpinionID_Opinion",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Opinion",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ID_Song",
                table: "Opinion",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Opinion",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Opinion_ApplicationUserId",
                table: "Opinion",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Opinion_ID_Song",
                table: "Opinion",
                column: "ID_Song");

            migrationBuilder.AddForeignKey(
                name: "FK_Opinion_AspNetUsers_ApplicationUserId",
                table: "Opinion",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Opinion_Song_ID_Song",
                table: "Opinion",
                column: "ID_Song",
                principalTable: "Song",
                principalColumn: "ID_Song",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Opinion_AspNetUsers_ApplicationUserId",
                table: "Opinion");

            migrationBuilder.DropForeignKey(
                name: "FK_Opinion_Song_ID_Song",
                table: "Opinion");

            migrationBuilder.DropIndex(
                name: "IX_Opinion_ApplicationUserId",
                table: "Opinion");

            migrationBuilder.DropIndex(
                name: "IX_Opinion_ID_Song",
                table: "Opinion");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Opinion");

            migrationBuilder.AddColumn<int>(
                name: "OpinionID_Opinion",
                table: "Song",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Opinion",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ID_Song",
                table: "Opinion",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OpinionID_Opinion",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Song_OpinionID_Opinion",
                table: "Song",
                column: "OpinionID_Opinion");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_OpinionID_Opinion",
                table: "AspNetUsers",
                column: "OpinionID_Opinion");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Opinion_OpinionID_Opinion",
                table: "AspNetUsers",
                column: "OpinionID_Opinion",
                principalTable: "Opinion",
                principalColumn: "ID_Opinion",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Song_Opinion_OpinionID_Opinion",
                table: "Song",
                column: "OpinionID_Opinion",
                principalTable: "Opinion",
                principalColumn: "ID_Opinion",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
