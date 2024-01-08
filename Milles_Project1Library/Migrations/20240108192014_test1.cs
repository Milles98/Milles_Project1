using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Milles_Project1Library.Migrations
{
    /// <inheritdoc />
    public partial class test1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GameStatisticsId",
                table: "Game",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GameStatistics",
                columns: table => new
                {
                    StatisticsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalGamesPlayed = table.Column<int>(type: "int", nullable: false),
                    TotalWins = table.Column<int>(type: "int", nullable: false),
                    TotalLosses = table.Column<int>(type: "int", nullable: false),
                    TotalDraws = table.Column<int>(type: "int", nullable: false),
                    AverageWins = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameStatistics", x => x.StatisticsId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Game_GameStatisticsId",
                table: "Game",
                column: "GameStatisticsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_GameStatistics_GameStatisticsId",
                table: "Game",
                column: "GameStatisticsId",
                principalTable: "GameStatistics",
                principalColumn: "StatisticsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_GameStatistics_GameStatisticsId",
                table: "Game");

            migrationBuilder.DropTable(
                name: "GameStatistics");

            migrationBuilder.DropIndex(
                name: "IX_Game_GameStatisticsId",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "GameStatisticsId",
                table: "Game");
        }
    }
}
