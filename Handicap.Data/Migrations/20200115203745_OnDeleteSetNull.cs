using Microsoft.EntityFrameworkCore.Migrations;

namespace Handicap.Data.Migrations
{
    public partial class OnDeleteSetNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Players_PlayerOneId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Players_PlayerTwoId",
                table: "Games");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Players_PlayerOneId",
                table: "Games",
                column: "PlayerOneId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Players_PlayerTwoId",
                table: "Games",
                column: "PlayerTwoId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Players_PlayerOneId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Players_PlayerTwoId",
                table: "Games");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Players_PlayerOneId",
                table: "Games",
                column: "PlayerOneId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Players_PlayerTwoId",
                table: "Games",
                column: "PlayerTwoId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
