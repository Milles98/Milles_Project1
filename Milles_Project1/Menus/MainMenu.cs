using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Milles_Project1Library.Data;
using Milles_Project1Library.ExtraServices;
using Autofac;
using Milles_Project1Library.Interfaces;

namespace Milles_Project1.Menus
{
    public static class MainMenu
    {
        public static void ShowMenu(ProjectDbContext dbContext, IShapeContext shapeContext, ICalculatorContext calculatorContext)
        {
            int choice;

            do
            {
                Console.Clear();
                Console.WriteLine("╭─────────────────────────╮");
                Console.WriteLine("│ Main Menu               │");
                Console.WriteLine("│ 1. Shapes               │");
                Console.WriteLine("│ 2. Calculator           │");
                Console.WriteLine("│ 3. Rock Paper Scissors  │");
                Console.WriteLine("│ 0. Exit Program         │");
                Console.WriteLine("╰─────────────────────────╯");

                Console.Write("Enter your choice: ");
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            ShapesMenu.ShowShapesMenu(shapeContext);
                            break;
                        case 2:
                            CalculatorMenu.ShowCalculatorMenu(calculatorContext);
                            break;
                        case 3:
                            GameMenu.ShowGameMenu(dbContext);
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
