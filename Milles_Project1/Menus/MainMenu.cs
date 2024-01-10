using Milles_Project1Library.ExtraServices;
using Milles_Project1Library.Interfaces.ServiceInterface;

namespace Milles_Project1.Menus
{
    public static class MainMenu
    {
        public static void ShowMenu(ICalculatorService calculatorService, IShapeService shapeService, IGameService gameService)
        {
            int choice;

            do
            {
                Console.Clear();
                Message.ProjectMessage();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("╭─────────────────────────╮");
                Console.WriteLine("│ Main Menu               │");
                Console.WriteLine("│ 1. Shapes               │");
                Console.WriteLine("│ 2. Calculator           │");
                Console.WriteLine("│ 3. Rock Paper Scissors  │");
                Console.WriteLine("│ 0. Exit Program         │");
                Console.WriteLine("╰─────────────────────────╯");
                Console.ResetColor();

                Console.Write("Enter your choice: ");
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            ShapesMenu.ShowShapesMenu(shapeService);
                            break;
                        case 2:
                            CalculatorMenu.ShowCalculatorMenu(calculatorService);
                            break;
                        case 3:
                            GameMenu.ShowGameMenu(gameService);
                            break;
                        case 0:
                            Console.WriteLine("Exiting program...");
                            Environment.Exit(0);
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
