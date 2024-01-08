using Milles_Project1Library.Data;
using Milles_Project1Library.ExtraServices;
using Milles_Project1Library.Interfaces.ServiceInterface;
using Milles_Project1Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milles_Project1Library.Services
{
    public class GameService : IGameService
    {
        private readonly ProjectDbContext _dbContext;

        public GameService(ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void PlayGame()
        {
            int playerWins = 0;
            int computerWins = 0;
            int rounds = 0;
            Game lastGame = null;

            do
            {
                Console.Clear();
                Console.WriteLine($"╭──────────────────────────╮");
                Console.WriteLine($"│Rock Paper Scissors Game  │");
                Message.DarkYellowMessage($"│ Round {rounds + 1}                  │");
                Console.WriteLine($"│ Player Wins: {playerWins}           │");
                Console.WriteLine($"│ Computer Wins: {computerWins}         │");
                Console.WriteLine($"╰──────────────────────────╯");

                Console.WriteLine("1. Rock");
                Console.WriteLine("2. Paper");
                Console.WriteLine("3. Scissors");
                Console.WriteLine("0. End the Game");

                Console.Write("Enter your move (1-3): ");
                if (int.TryParse(Console.ReadLine(), out int playerChoice))
                {
                    if (playerChoice >= 1 && playerChoice <= 3)
                    {
                        Move playerMove = (Move)(playerChoice - 1);

                        Move computerMove = GenerateComputerMove();

                        GameResult result = DetermineResult(playerMove, computerMove);

                        DisplayGameResult(playerMove, computerMove, result);

                        if (result == GameResult.Win)
                        {
                            playerWins++;
                        }
                        else if (result == GameResult.Loss)
                        {
                            computerWins++;
                        }

                        lastGame = new Game
                        {
                            PlayerMove = playerMove,
                            ComputerMove = computerMove,
                            Result = result,
                            GameDate = DateTime.Now,
                            IsActive = true
                        };

                        rounds++;

                        Console.WriteLine("\nPress Enter to continue...");
                        Console.ReadLine();
                    }
                    else if (playerChoice == 0)
                    {
                        Console.WriteLine("Ending the Game...");
                        break;
                    }
                    else
                    {
                        Message.ErrorMessage("Invalid choice. Please enter a number between 1 and 3.");
                        Thread.Sleep(1000);
                    }
                }
                else
                {
                    Message.ErrorMessage("Invalid input. Please enter a number.");
                    Thread.Sleep(1000);
                }

            } while (playerWins < 2 && computerWins < 2);

            string overallWinner = (playerWins >= 2) ? "Player" : "Computer";

            Message.InputSuccessMessage($"\nOverall Winner: {overallWinner}");

            // Save only the last game played
            if (lastGame != null)
            {
                SaveGameHistory(lastGame, rounds);
            }

            Console.Write("\nDo you want to play again? (Y/N): ");
            string playAgainInput = Console.ReadLine();

            if (playAgainInput?.Trim().ToUpper() == "Y")
            {
                PlayGame();
            }
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
            // Save the Game record first
            _dbContext.Game.Add(game);
            _dbContext.SaveChanges();

            // Create a new GameHistory record with the generated GameId
            var newGameHistory = new GameHistory
            {
                GameId = game.GameId,
                Winner = (game.Result == GameResult.Win) ? "Player" : (game.Result == GameResult.Loss) ? "Computer" : "Draw",
                RoundsTaken = rounds,
                WinningMove = (game.Result == GameResult.Win) ? game.PlayerMove : game.ComputerMove,
                GameEndDate = DateTime.Now
            };

            // Save the GameHistory record
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
                    Message.InputSuccessMessage("You Win!");
                    break;
                case GameResult.Loss:
                    Message.ErrorMessage("Computer Wins!");
                    break;
                case GameResult.Draw:
                    Message.DarkYellowMessage("It's a Draw!");
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

            var previousGames = _dbContext.Game.OrderByDescending(g => g.GameDate).ToList();

            if (previousGames.Any())
            {
                foreach (var game in previousGames)
                {
                    Console.WriteLine($"Game ID: {game.GameId}");
                    Console.WriteLine($"Date: {game.GameDate}");
                    Console.WriteLine($"Your Last Move: {game.PlayerMove}");
                    Console.WriteLine($"Computer's Last Move: {game.ComputerMove}");
                    Console.WriteLine($"Final Result: {game.Result}");

                    var gameHistory = _dbContext.GameHistory.FirstOrDefault(gh => gh.GameId == game.GameId);

                    if (gameHistory != null)
                    {
                        Console.WriteLine($"Winner: {gameHistory.Winner}");
                        Console.WriteLine($"Rounds Taken: {gameHistory.RoundsTaken}");
                        Console.WriteLine($"Winning Move: {gameHistory.WinningMove}");
                        Console.WriteLine($"Game End Date: {gameHistory.GameEndDate}");
                    }

                    Console.WriteLine($"-----------------------------");
                }
            }
            else
            {
                Console.WriteLine("No previous games found.");
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

            Console.WriteLine("Rock-Paper-Scissors is a game played to settle disputes between two people.");
            Console.WriteLine("It's often taught to children to help them resolve arguments without adult intervention.");
            Console.WriteLine("However, the game involves an element of skill that requires quick thinking and reasoning.");

            Console.WriteLine("\nThe game is played with three possible hand signals:");
            Console.WriteLine("- Rock: A closed fist");
            Console.WriteLine("- Paper: A flat hand with fingers and thumb extended, palm facing downward");
            Console.WriteLine("- Scissors: A fist with the index and middle fingers fully extended");

            Console.WriteLine("\nGame Rules:");
            Console.WriteLine("- Rock wins against scissors");
            Console.WriteLine("- Paper wins against rock");
            Console.WriteLine("- Scissors wins against paper");
            Console.WriteLine("- If both players throw the same hand signal, it's considered a tie");

            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }
    }
}
