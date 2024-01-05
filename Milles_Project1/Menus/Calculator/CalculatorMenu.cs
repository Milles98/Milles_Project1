using Milles_Project1Library.ExtraServices;
using Milles_Project1Library.Services.CalculatorStrategyService;
using Milles_Project1Library.StrategyContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milles_Project1.Menus.Calculator
{
    public static class CalculatorMenu
    {
        public static void ShowCalculatorMenu()
        {
            var calculator = new CalculatorContext(new AdditionStrategy());
            int choice;

            do
            {
                Console.Clear();
                Console.WriteLine("╭──────────────────────╮");
                Console.WriteLine("│Calculator Menu       │");
                Console.WriteLine("│1. (+) Addition       │");
                Console.WriteLine("│2. (-) Subtraction    │");
                Console.WriteLine("│3. (*) Multiplication │");
                Console.WriteLine("│4. (/) Divison        │");
                Console.WriteLine("│5. (√) Power of       │");
                Console.WriteLine("│6. (%) Modulus        │");
                Console.WriteLine("│0. Return to MainMenu │");
                Console.WriteLine("╰──────────────────────╯");

                Console.Write("Enter your choice: ");
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            calculator.SetStrategy(new AdditionStrategy());
                            PerformCalculation(calculator);
                            break;
                        case 2:
                            calculator.SetStrategy(new SubtractionStrategy());
                            PerformCalculation(calculator);
                            break;
                        case 3:
                            calculator.SetStrategy(new MultiplicationStrategy());
                            PerformCalculation(calculator);
                            break;
                        case 4:
                            calculator.SetStrategy(new DivisionStrategy());
                            PerformCalculation(calculator);
                            break;
                        case 5:
                            calculator.SetStrategy(new PowerOfStrategy());
                            PerformCalculation(calculator);
                            break;
                        case 6:
                            calculator.SetStrategy(new ModulusStrategy());
                            PerformCalculation(calculator);
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

        private static void PerformCalculation(CalculatorContext calculator)
        {
            Console.Write("Enter the first number: ");
            if (double.TryParse(Console.ReadLine(), out double num1))
            {
                Console.Write("Enter the second number: ");
                if (double.TryParse(Console.ReadLine(), out double num2))
                {
                    double result = calculator.ExecuteOperation(num1, num2);
                    Console.WriteLine($"Result: {result}");
                }
                else
                {
                    Message.ErrorMessage("Invalid input for the second number. Please enter a valid number.");
                    Thread.Sleep(1000);
                }
            }
            else
            {
                Message.ErrorMessage("Invalid input for the first number. Please enter a valid number.");
                Thread.Sleep(1000);
            }

            Console.WriteLine("Press any button to continue.");
            Console.ReadKey();
        }
    }
}
