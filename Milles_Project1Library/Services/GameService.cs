﻿using Autofac;
using Microsoft.EntityFrameworkCore;
using Milles_Project1Library.Data;
using Milles_Project1Library.ExtraServices;
using Milles_Project1Library.Interfaces.ServiceInterface;
using Milles_Project1Library.Models;

namespace Milles_Project1Library.Services
{
    public class GameService : IGameService
    {
        private readonly ProjectDbContext _dbContext;

        public GameService(ILifetimeScope lifetimeScope)
        {
            _dbContext = lifetimeScope.Resolve<ProjectDbContext>();
        }

        public void PlayGame()
        {
            bool keepPlaying = true;

            while (keepPlaying)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine($"╭──────────────────────────╮");
                Console.WriteLine($"│Rock Paper Scissors Game  │");
                Console.WriteLine($"╰──────────────────────────╯");

                Console.WriteLine($"╭──────────────────────────╮");
                Console.WriteLine("│1. Rock                   │");
                Console.WriteLine("│2. Paper                  │");
                Console.WriteLine("│3. Scissors               │");
                Console.WriteLine("│0. End the Game           │");
                Console.WriteLine($"╰──────────────────────────╯");
                Console.ResetColor();

                Console.Write("\nEnter your move (0-3): ");
                if (int.TryParse(Console.ReadLine(), out int playerChoice))
                {
                    if (playerChoice == 0)
                    {
                        keepPlaying = false;
                        continue;
                    }
                    if (playerChoice >= 1 && playerChoice <= 3)
                    {
                        Move playerMove = (Move)(playerChoice - 1);
                        Move computerMove = GenerateComputerMove();
                        GameResult result = DetermineResult(playerMove, computerMove);

                        DisplayGameResult(playerMove, computerMove, result);

                        var game = new Game
                        {
                            PlayerMove = playerMove,
                            ComputerMove = computerMove,
                            Result = result,
                            GameDate = DateTime.Now,
                            IsActive = true
                        };

                        var gameStatistics = _dbContext.GameStatistics.FirstOrDefault();
                        if (gameStatistics == null)
                        {
                            gameStatistics = new GameStatistics();
                            _dbContext.GameStatistics.Add(gameStatistics);
                        }

                        UpdateGameStatistics(result, gameStatistics);
                        SaveGameHistory(game, 1);
                    }
                    else
                    {
                        Message.RedMessage("Invalid choice. Please enter a number between 0 and 3.");
                        Thread.Sleep(1000);
                    }
                }
                else
                {
                    Message.RedMessage("Invalid input. Please enter a number.");
                    Thread.Sleep(1000);
                }

                Console.WriteLine("Press any key to play again.");
                Console.ReadKey();
            }
        }




        private void UpdateGameStatistics(GameResult result, GameStatistics statistics)
        {
            statistics.TotalGamesPlayed++;
            if (result == GameResult.Win)
            {
                statistics.TotalWins++;
            }
            else if (result == GameResult.Loss)
            {
                statistics.TotalLosses++;
            }
            else
            {
                statistics.TotalDraws++;
            }

            statistics.AverageWins = (double)statistics.TotalWins / statistics.TotalGamesPlayed;

            _dbContext.SaveChanges();
        }

        private Move GenerateComputerMove()
        {
            Random random = new Random();
            return (Move)random.Next(0, 3);
        }

        private GameResult DetermineResult(Move playerMove, Move computerMove)
        {
            if (playerMove == computerMove)
            {
                return GameResult.Draw;
            }
            else if (
                (playerMove == Move.Rock && computerMove == Move.Scissor) ||
                (playerMove == Move.Scissor && computerMove == Move.Paper) ||
                (playerMove == Move.Paper && computerMove == Move.Rock)
            )
            {
                return GameResult.Win;
            }
            else
            {
                return GameResult.Loss;
            }
        }

        private void SaveGameHistory(Game game, int rounds)
        {
            _dbContext.Game.Add(game);
            _dbContext.SaveChanges();

            var newGameHistory = new GameHistory
            {
                GameId = game.GameId,
                Winner = (game.Result == GameResult.Win) ? "Player" : (game.Result == GameResult.Loss) ? "Computer" : "Draw",
                RoundsTaken = rounds,
                WinningMove = (game.Result == GameResult.Win) ? game.PlayerMove : game.ComputerMove,
                GameEndDate = DateTime.Now
            };

            _dbContext.GameHistory.Add(newGameHistory);
            _dbContext.SaveChanges();
        }


        private void DisplayGameResult(Move playerMove, Move computerMove, GameResult result)
        {
            Console.WriteLine($"\nYour move: {playerMove}");
            Console.WriteLine($"Computer's move: {computerMove}");

            switch (result)
            {
                case GameResult.Win:
                    Message.GreenMessage($"╭────────╮");
                    Message.GreenMessage("│You Win!│");
                    Message.GreenMessage($"╰────────╯");
                    break;
                case GameResult.Loss:
                    Message.RedMessage($"╭──────────────╮");
                    Message.RedMessage("│Computer Wins!│");
                    Message.RedMessage($"╰──────────────╯");
                    break;
                case GameResult.Draw:
                    Message.DarkYellowMessage($"╭────────────╮");
                    Message.DarkYellowMessage("│It's a Draw!│");
                    Message.DarkYellowMessage($"╰────────────╯");
                    break;
                default:
                    break;
            }
        }

        public void ViewPreviousGames()
        {
            Console.Clear();
            Console.WriteLine("╭───────────────────────────────╮");
            Console.WriteLine("│   View Previous Games         │");
            Console.WriteLine("╰───────────────────────────────╯");

            var previousGames = _dbContext.Game
                .OrderBy(g => g.GameDate)
                .Include(g => g.GameHistories)
                .ToList();

            if (previousGames.Any())
            {
                foreach (var game in previousGames)
                {
                    Console.WriteLine($"Game ID: {game.GameId}");
                    Console.WriteLine($"Your Move: {game.PlayerMove}");
                    Console.WriteLine($"Computer's Move: {game.ComputerMove}");
                    Console.WriteLine($"Your Result: {game.Result}");

                    if (game.GameHistories != null && game.GameHistories.Any())
                    {
                        Console.WriteLine($"Winner: {game.GameHistories.LastOrDefault()?.Winner}");
                        Console.WriteLine($"Game End Date: {game.GameHistories.LastOrDefault()?.GameEndDate}");
                    }

                    Console.WriteLine("──────────────────────────────────────");
                }

                var totalWins = previousGames.Sum(g => g.GameHistories?.Count(gh => gh.Winner == "Player") ?? 0);
                var totalLosses = previousGames.Sum(g => g.GameHistories?.Count(gh => gh.Winner == "Computer") ?? 0);
                var totalDraws = previousGames.Sum(g => g.GameHistories?.Count(gh => gh.Winner == "Draw") ?? 0);

                var totalGames = previousGames.Count;
                var averageWins = totalGames > 0 ? (double)totalWins / totalGames : 0;

                Message.GreenMessage($"Total Wins: {totalWins}");
                Message.RedMessage($"Total Losses: {totalLosses}");
                Message.DarkYellowMessage($"Total Draws: {totalDraws}");
                Console.WriteLine($"Average Wins Against Computer: {averageWins:P}");
                Console.WriteLine("──────────────────────────────────────");
            }
            else
            {
                Message.RedMessage("No previous games found.");
            }

            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }

        public void GameRules()
        {
            Console.Clear();
            Console.WriteLine("╭───────────────────────────────╮");
            Console.WriteLine("│      Rock-Paper-Scissors      │");
            Console.WriteLine("│         How to Play           │");
            Console.WriteLine("╰───────────────────────────────╯");

            Message.DarkYellowMessage("\nThe game is played with three possible hand signals:");
            Console.WriteLine("- Rock");
            Console.WriteLine("- Paper");
            Console.WriteLine("- Scissors");

            Message.DarkYellowMessage("\nGame Rules:");
            Console.WriteLine("- Rock wins against scissors");
            Console.WriteLine("- Paper wins against rock");
            Console.WriteLine("- Scissors wins against paper");
            Console.WriteLine("- If both players throw the same hand signal, it's considered a tie");

            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }
    }
}
