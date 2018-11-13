using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace inz.Data.Migrations
{
    public partial class producer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ID_Producer",
                table: "Song",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Producer",
                columns: table => new
                {
                    ID_Producer = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name_Producer = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producer", x => x.ID_Producer);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Song_ID_Producer",
                table: "Song",
                column: "ID_Producer");

            migrationBuilder.AddForeignKey(
                name: "FK_Song_Producer_ID_Producer",
                table: "Song",
                column: "ID_Producer",
                principalTable: "Producer",
                principalColumn: "ID_Producer",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Song_Producer_ID_Producer",
                table: "Song");

            migrationBuilder.DropTable(
                name: "Producer");

            migrationBuilder.DropIndex(
                name: "IX_Song_ID_Producer",
                table: "Song");

            migrationBuilder.DropColumn(
                name: "ID_Producer",
                table: "Song");
        }
    }
}
