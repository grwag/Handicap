using Microsoft.EntityFrameworkCore.Migrations;

namespace Handicap.Data.Migrations
{
    public partial class maxGames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "HandicapConfigurations",
                keyColumn: "Id",
                keyValue: "99");

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: "4");

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: "5");

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: "6");

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: "7");

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: "8");

            migrationBuilder.AddColumn<int>(
                name: "EightBallMax",
                table: "HandicapConfigurations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NineBallMax",
                table: "HandicapConfigurations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StraigntPoolMax",
                table: "HandicapConfigurations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TenBallMax",
                table: "HandicapConfigurations",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EightBallMax",
                table: "HandicapConfigurations");

            migrationBuilder.DropColumn(
                name: "NineBallMax",
                table: "HandicapConfigurations");

            migrationBuilder.DropColumn(
                name: "StraigntPoolMax",
                table: "HandicapConfigurations");

            migrationBuilder.DropColumn(
                name: "TenBallMax",
                table: "HandicapConfigurations");

            migrationBuilder.InsertData(
                table: "HandicapConfigurations",
                columns: new[] { "Id", "TenantId", "UpdatePlayersImmediately" },
                values: new object[] { "99", "", false });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "FirstName", "Handicap", "LastName", "TenantId" },
                values: new object[,]
                {
                    { "1", "alf", 65, "ralf", "816ef7d5-4589-4408-b64c-87594e2075bb" },
                    { "2", "hans", 35, "maulwurf", "816ef7d5-4589-4408-b64c-87594e2075bb" },
                    { "3", "karl", 30, "klammer", "816ef7d5-4589-4408-b64c-87594e2075bb" },
                    { "4", "bart", 55, "simpson", "816ef7d5-4589-4408-b64c-87594e2075bb" },
                    { "5", "nasen", 25, "baer", "" },
                    { "6", "eier", 5, "kopf", "" },
                    { "7", "rudi", 30, "rakete", "" },
                    { "8", "homer", 55, "simpson", "" }
                });
        }
    }
}
