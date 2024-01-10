using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Milles_Project1Library.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Calculator",
                columns: table => new
                {
                    CalculationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number1 = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    Number2 = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    Operator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Result = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    CalculationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calculator", x => x.CalculationId);
                });

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

            migrationBuilder.CreateTable(
                name: "Shape",
                columns: table => new
                {
                    ShapeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShapeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Base = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    Height = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    SideLength = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    Area = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    Perimeter = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    CalculationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shape", x => x.ShapeId);
                });

            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerMove = table.Column<int>(type: "int", nullable: false),
                    ComputerMove = table.Column<int>(type: "int", nullable: false),
                    Result = table.Column<int>(type: "int", nullable: false),
                    GameDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    AverageWins = table.Column<double>(type: "float", nullable: false),
                    GameStatisticsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.GameId);
                    table.ForeignKey(
                        name: "FK_Game_GameStatistics_GameStatisticsId",
                        column: x => x.GameStatisticsId,
                        principalTable: "GameStatistics",
                        principalColumn: "StatisticsId");
                });

            migrationBuilder.CreateTable(
                name: "GameHistory",
                columns: table => new
                {
                    GameHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    Winner = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoundsTaken = table.Column<int>(type: "int", nullable: false),
                    WinningMove = table.Column<int>(type: "int", nullable: false),
                    GameEndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameHistory", x => x.GameHistoryId);
                    table.ForeignKey(
                        name: "FK_GameHistory_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Game_GameStatisticsId",
                table: "Game",
                column: "GameStatisticsId");

            migrationBuilder.CreateIndex(
                name: "IX_GameHistory_GameId",
                table: "GameHistory",
                column: "GameId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Calculator");

            migrationBuilder.DropTable(
                name: "GameHistory");

            migrationBuilder.DropTable(
                name: "Shape");

            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "GameStatistics");
        }
    }
}
