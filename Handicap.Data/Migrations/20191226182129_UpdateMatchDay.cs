using Microsoft.EntityFrameworkCore.Migrations;

namespace Handicap.Data.Migrations
{
    public partial class UpdateMatchDay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFinished",
                table: "MatchDays",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFinished",
                table: "MatchDays");
        }
    }
}
