using Milles_Project1Library.ExtraServices;
using Milles_Project1Library.Interfaces.FactoryInterface;
using Milles_Project1Library.Interfaces.ServiceInterface;

namespace Milles_Project1Library.Menus
{
    public class GameMenu : IMenu
    {
        private readonly IGameService _gameService;

        public GameMenu(IGameService gameService)
        {
            _gameService = gameService;
        }

        public void ShowMenu()
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
                            _gameService.PlayGame();
                            break;
                        case 2:
                            _gameService.GameRules();
                            break;
                        case 3:
                            _gameService.ViewPreviousGames();
                            break;
                        case 0:
                            Console.WriteLine("Returning to MainMenu...");
                            break;
                        default:
                            Message.RedMessage("Invalid choice. Please try again.");
                            Thread.Sleep(1000);
                            break;
                    }
                }
                else
                {
                    Message.RedMessage("Invalid input. Please enter a number.");
                    Thread.Sleep(1000);
                }

            } while (choice != 0);
        }
        public Type GetMenuType()
        {
            return this.GetType();
        }
    }
}
