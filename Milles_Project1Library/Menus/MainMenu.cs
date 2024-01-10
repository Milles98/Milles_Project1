using Milles_Project1Library.ExtraServices;
using Milles_Project1Library.Interfaces.FactoryInterface;
using Autofac;

namespace Milles_Project1Library.Menus
{
    public static class MainMenu
    {
        public static void ShowMenu(IContainer container)
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
                            var shapesMenu = container.ResolveNamed<IMenuFactory>("ShapeMenuFactory").CreateMenu();
                            if (shapesMenu.GetMenuType() == typeof(ShapesMenu))
                            {
                                ((ShapesMenu)shapesMenu).ShowMenu();
                            }
                            break;
                        case 2:
                            var calculatorMenu = container.ResolveNamed<IMenuFactory>("CalculatorMenuFactory").CreateMenu();
                            if (calculatorMenu.GetMenuType() == typeof(CalculatorMenu))
                            {
                                ((CalculatorMenu)calculatorMenu).ShowMenu();
                            }
                            break;
                        case 3:
                            var gameMenu = container.ResolveNamed<IMenuFactory>("GameMenuFactory").CreateMenu();
                            if (gameMenu.GetMenuType() == typeof(GameMenu))
                            {
                                ((GameMenu)gameMenu).ShowMenu();
                            }
                            break;
                        case 0:
                            Console.WriteLine("Exiting program...");
                            Environment.Exit(0);
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
    }
}
