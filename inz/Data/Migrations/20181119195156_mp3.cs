using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace inz.Data.Migrations
{
    public partial class mp3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ID_Mp3",
                table: "Song",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Mp3",
                columns: table => new
                {
                    ID_Mp3 = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    mp3 = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mp3", x => x.ID_Mp3);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Song_ID_Mp3",
                table: "Song",
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

            migrationBuilder.DropTable(
                name: "Mp3");

            migrationBuilder.DropIndex(
                name: "IX_Song_ID_Mp3",
                table: "Song");

            migrationBuilder.DropColumn(
                name: "ID_Mp3",
                table: "Song");
        }
    }
}
