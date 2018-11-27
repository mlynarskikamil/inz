using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace inz.Data.Migrations
{
    public partial class mp3change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name_mp3",
                table: "Mp3",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Name_mp3",
                table: "Mp3",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
