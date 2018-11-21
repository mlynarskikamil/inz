using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace inz.Data.Migrations
{
    public partial class mp33 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Song_mp3_ID_Mp3",
                table: "Song");

            migrationBuilder.DropPrimaryKey(
                name: "PK_mp3",
                table: "mp3");

            migrationBuilder.RenameTable(
                name: "mp3",
                newName: "Mp3");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Mp3",
                table: "Mp3",
                column: "ID_Mp3");

            migrationBuilder.AddForeignKey(
                name: "FK_Song_Mp3_ID_Mp3",
                table: "Song",
                column: "ID_Mp3",
                principalTable: "Mp3",
                principalColumn: "ID_Mp3",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Song_Mp3_ID_Mp3",
                table: "Song");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Mp3",
                table: "Mp3");

            migrationBuilder.RenameTable(
                name: "Mp3",
                newName: "mp3");

            migrationBuilder.AddPrimaryKey(
                name: "PK_mp3",
                table: "mp3",
                column: "ID_Mp3");

            migrationBuilder.AddForeignKey(
                name: "FK_Song_mp3_ID_Mp3",
                table: "Song",
                column: "ID_Mp3",
                principalTable: "mp3",
                principalColumn: "ID_Mp3",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
