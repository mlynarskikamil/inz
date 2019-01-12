using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace inz.Data.Migrations
{
    public partial class lubie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OpinionID_Opinion",
                table: "Song",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OpinionID_Opinion",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Opinion",
                columns: table => new
                {
                    ID_Opinion = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Direction = table.Column<bool>(nullable: false),
                    ID_Song = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opinion", x => x.ID_Opinion);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Opinion_OpinionID_Opinion",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Song_Opinion_OpinionID_Opinion",
                table: "Song");

            migrationBuilder.DropTable(
                name: "Opinion");

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
        }
    }
}
