using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace inz.Data.Migrations
{
    public partial class Init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
                       
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Song_Artist_ID_Artist",
                table: "Song");

            migrationBuilder.DropIndex(
                name: "IX_Song_ID_Artist",
                table: "Song");

            migrationBuilder.AddColumn<int>(
                name: "ID_Feat_Artist",
                table: "Song",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Song_ID_Feat_Artist",
                table: "Song",
                column: "ID_Feat_Artist");

            migrationBuilder.AddForeignKey(
                name: "FK_Song_Artist_ID_Feat_Artist",
                table: "Song",
                column: "ID_Feat_Artist",
                principalTable: "Artist",
                principalColumn: "ID_Artist",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
