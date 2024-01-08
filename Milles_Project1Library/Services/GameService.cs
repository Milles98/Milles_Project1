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

            do
            {
                Console.Clear();
                Console.WriteLine($"╭──────────────────────────╮");
                Console.WriteLine($"│Rock Paper Scissors Game  │");
                Console.WriteLine($"│ Round {rounds + 1}                  │");
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

                        SaveGame(playerMove, computerMove, result);

                        DisplayGameResult(playerMove, computerMove, result);

                        if (result == GameResult.Win)
                        {
                            playerWins++;
                        }
                        else if (result == GameResult.Loss)
                        {
                            computerWins++;
                        }

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

        private void SaveGame(Move playerMove, Move computerMove, GameResult result)
        {
            var newGame = new Game
            {
                PlayerMove = playerMove,
                ComputerMove = computerMove,
                Result = result,
                GameDate = DateTime.Now,
                IsActive = true
            };

            _dbContext.Game.Add(newGame);
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
    }
}
