using Milles_Project1Library.ExtraServices;
using Milles_Project1Library.Interfaces.FactoryInterface;
using Milles_Project1Library.Interfaces.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milles_Project1Library.Menus
{
    public class CalculatorMenu : IMenu
    {
        private readonly ICalculatorService _calculatorService;

        public CalculatorMenu(ICalculatorService calculatorService)
        {
            _calculatorService = calculatorService;
        }

        public void ShowMenu()
        {
            int choice;

            do
            {
                Console.Clear();
                Message.ProjectMessage();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("╭──────────────────────╮");
                Console.WriteLine("│Calculator Menu       │");
                Console.WriteLine("│1. Create             │");
                Console.WriteLine("│2. Read               │");
                Console.WriteLine("│3. Update             │");
                Console.WriteLine("│4. Delete             │");
                Console.WriteLine("│0. Return to MainMenu │");
                Console.WriteLine("╰──────────────────────╯");
                Console.ResetColor();

                Console.Write("Enter your choice: ");
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            _calculatorService.PerformCreateCalculation();
                            break;
                        case 2:
                            _calculatorService.ReadCalculation();
                            Console.WriteLine("Press any key to continue.");
                            Console.ReadKey();
                            break;
                        case 3:
                            _calculatorService.UpdateCalculation();
                            break;
                        case 4:
                            _calculatorService.DeleteCalculation();
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
