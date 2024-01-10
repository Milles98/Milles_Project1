using Milles_Project1Library.ExtraServices;
using Milles_Project1Library.Interfaces.ServiceInterface;

namespace Milles_Project1.Menus
{
    public static class GameMenu
    {
        public static void ShowGameMenu(IGameService gameService)
        {
            int choice;

            do
            {
                Console.Clear();
                Message.ProjectMessage();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("╭──────────────────────────╮");
                Console.WriteLine("│Rock Paper Scissors Menu  │");
                Console.WriteLine("│1. Play Game              │");
                Console.WriteLine("│2. Game Rules             │");
                Console.WriteLine("│3. View Previous Games    │");
                Console.WriteLine("│0. Return to MainMenu     │");
                Console.WriteLine("╰──────────────────────────╯");
                Console.ResetColor();

                Console.Write("Enter your choice: ");
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            gameService.PlayGame();
                            break;
                        case 2:
                            gameService.GameRules();
                            break;
                        case 3:
                            gameService.ViewPreviousGames();
                            break;
                        case 0:
                            Console.WriteLine("Returning to MainMenu...");
                            break;
                        default:
                            Message.ErrorMessage("Invalid choice. Please try again.");
                            Thread.Sleep(1000);
                            break;
                    }
                }
                else
                {
                    Message.ErrorMessage("Invalid input. Please enter a number.");
                    Thread.Sleep(1000);
                }

            } while (choice != 0);
        }
    }
}
