using Microsoft.EntityFrameworkCore.Migrations;

namespace Mobi.Migrations
{
    public partial class ufbugfix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFavorites_CustomProperties_CustomPropertyId",
                table: "UserFavorites");

            migrationBuilder.DropIndex(
                name: "IX_UserFavorites_CustomPropertyId",
                table: "UserFavorites");

            migrationBuilder.DropColumn(
                name: "CustomPropertyId",
                table: "UserFavorites");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomPropertyId",
                table: "UserFavorites",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserFavorites_CustomPropertyId",
                table: "UserFavorites",
                column: "CustomPropertyId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFavorites_CustomProperties_CustomPropertyId",
                table: "UserFavorites",
                column: "CustomPropertyId",
                principalTable: "CustomProperties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
