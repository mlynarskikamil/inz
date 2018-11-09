using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace inz.Data.Migrations
{
    public partial class Init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Song_Album_ID_Album",
                table: "Song");

            migrationBuilder.DropForeignKey(
                name: "FK_Song_Artist_ID_Artist",
                table: "Song");

            migrationBuilder.AlterColumn<int>(
                name: "ID_Artist",
                table: "Song",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ID_Album",
                table: "Song",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Song_Album_ID_Album",
                table: "Song",
                column: "ID_Album",
                principalTable: "Album",
                principalColumn: "ID_Album",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Song_Artist_ID_Artist",
                table: "Song",
                column: "ID_Artist",
                principalTable: "Artist",
                principalColumn: "ID_Artist",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Song_Album_ID_Album",
                table: "Song");

            migrationBuilder.DropForeignKey(
                name: "FK_Song_Artist_ID_Artist",
                table: "Song");

            migrationBuilder.AlterColumn<int>(
                name: "ID_Artist",
                table: "Song",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ID_Album",
                table: "Song",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Song_Album_ID_Album",
                table: "Song",
                column: "ID_Album",
                principalTable: "Album",
                principalColumn: "ID_Album",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Song_Artist_ID_Artist",
                table: "Song",
                column: "ID_Artist",
                principalTable: "Artist",
                principalColumn: "ID_Artist",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
