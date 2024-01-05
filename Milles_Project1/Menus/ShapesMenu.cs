using Milles_Project1Library.ExtraServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milles_Project1.Menus
{
    public static class ShapesMenu
    {
        public static void ShowShapesMenu()
        {
            int choice;

            do
            {
                Console.Clear();
                Console.WriteLine("╭──────────────────────╮");
                Console.WriteLine("│Shapes Menu           │");
                Console.WriteLine("│1. Calculate shape    │");
                Console.WriteLine("│0. Return to MainMenu │");
                Console.WriteLine("╰──────────────────────╯");

                Console.Write("Enter your choice: ");
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
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
