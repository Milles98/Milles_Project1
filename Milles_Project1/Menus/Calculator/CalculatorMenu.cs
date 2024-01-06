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

namespace Milles_Project1.Menus.Calculator
{
    public static class CalculatorMenu
    {
        public static void ShowCalculatorMenu(ICalculatorContext calculatorContext)
        {
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
                            calculatorContext.SetStrategy(new AdditionStrategy());
                            PerformCalculation(calculatorContext);
                            break;
                        case 2:
                            calculatorContext.SetStrategy(new SubtractionStrategy());
                            PerformCalculation(calculatorContext);
                            break;
                        case 3:
                            calculatorContext.SetStrategy(new MultiplicationStrategy());
                            PerformCalculation(calculatorContext);
                            break;
                        case 4:
                            calculatorContext.SetStrategy(new DivisionStrategy());
                            PerformCalculation(calculatorContext);
                            break;
                        case 5:
                            calculatorContext.SetStrategy(new PowerOfStrategy());
                            PerformCalculation(calculatorContext);
                            break;
                        case 6:
                            calculatorContext.SetStrategy(new ModulusStrategy());
                            PerformCalculation(calculatorContext);
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

        private static void PerformCalculation(ICalculatorContext calculatorContext)
        {
            Console.Write("Enter the first number: ");
            if (double.TryParse(Console.ReadLine(), out double num1))
            {
                Console.Write("Enter the second number: ");
                if (double.TryParse(Console.ReadLine(), out double num2))
                {
                    double result = calculatorContext.ExecuteOperation(num1, num2);
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
