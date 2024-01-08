using Milles_Project1Library.ExtraServices;
using Milles_Project1Library.Interfaces;
using Milles_Project1Library.Services.CalculatorStrategyService;
using Milles_Project1Library.StrategyContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Milles_Project1Library.Data;
using Milles_Project1Library.Services;
using Milles_Project1Library.Interfaces.ServiceInterface;

namespace Milles_Project1.Menus
{
    public static class CalculatorMenu
    {
        public static void ShowCalculatorMenu(ICalculatorContext calculatorContext, ICalculatorService calculatorService)
        {
            int choice;

            do
            {
                Console.Clear();
                Console.WriteLine("╭──────────────────────╮");
                Console.WriteLine("│Calculator Menu       │");
                Console.WriteLine("│1. Create Calculator  │"); //ska vara att göra beräkning
                Console.WriteLine("│2. Read Calculator    │"); //läsa alla beräkningar
                Console.WriteLine("│3. Update Calculator  │"); //uppdatera en beräkning
                Console.WriteLine("│4. Delete Calculator  │"); //radera en beräkning
                Console.WriteLine("│0. Return to MainMenu │");
                Console.WriteLine("╰──────────────────────╯");

                Console.Write("Enter your choice: ");
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            calculatorService.PerformCreateCalculation();
                            break;
                        case 2:
                            calculatorService.ReadCalculation();
                            Console.ReadKey();
                            break;
                        case 3:
                            calculatorService.UpdateCalculation();
                            break;
                        case 4:
                            calculatorService.DeleteCalculation();
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
