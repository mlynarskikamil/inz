using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace inz.Data.Migrations
{
    public partial class fastfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Favorite_ID_Song",
                table: "Favorite");

            migrationBuilder.CreateIndex(
                name: "IX_Favorite_ID_Song",
                table: "Favorite",
                column: "ID_Song",
                unique: true,
                filter: "[ID_Song] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Favorite_ID_Song",
                table: "Favorite");

            migrationBuilder.CreateIndex(
                name: "IX_Favorite_ID_Song",
                table: "Favorite",
                column: "ID_Song");
        }
    }
}
