using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace inz.Data.Migrations
{
    public partial class log2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Changelog_AspNetUsers_ApplicationUserId",
                table: "Changelog");

            migrationBuilder.DropForeignKey(
                name: "FK_Changelog_Changelog_Name_List_ID_Changelog_Event",
                table: "Changelog");

            migrationBuilder.DropTable(
                name: "Changelog_Name_List");

            migrationBuilder.DropIndex(
                name: "IX_Changelog_ApplicationUserId",
                table: "Changelog");

            migrationBuilder.DropIndex(
                name: "IX_Changelog_ID_Changelog_Event",
                table: "Changelog");

            migrationBuilder.DropColumn(
                name: "ID_Changelog_Event",
                table: "Changelog");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Changelog",
                newName: "Changelog_Event");

            migrationBuilder.AlterColumn<string>(
                name: "Changelog_Event",
                table: "Changelog",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Changelog_Event",
                table: "Changelog",
                newName: "ApplicationUserId");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Changelog",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ID_Changelog_Event",
                table: "Changelog",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Changelog_Name_List",
                columns: table => new
                {
                    ID_Changelog_Event = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name_change = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Changelog_Name_List", x => x.ID_Changelog_Event);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Changelog_ApplicationUserId",
                table: "Changelog",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Changelog_ID_Changelog_Event",
                table: "Changelog",
                column: "ID_Changelog_Event");

            migrationBuilder.AddForeignKey(
                name: "FK_Changelog_AspNetUsers_ApplicationUserId",
                table: "Changelog",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Changelog_Changelog_Name_List_ID_Changelog_Event",
                table: "Changelog",
                column: "ID_Changelog_Event",
                principalTable: "Changelog_Name_List",
                principalColumn: "ID_Changelog_Event",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
