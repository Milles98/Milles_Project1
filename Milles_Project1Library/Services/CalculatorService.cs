using Autofac;
using Microsoft.EntityFrameworkCore;
using Milles_Project1Library.Data;
using Milles_Project1Library.ExtraServices;
using Milles_Project1Library.Interfaces.ContextInterface;
using Milles_Project1Library.Interfaces.ServiceInterface;
using Milles_Project1Library.Interfaces.StrategyInterface;
using Milles_Project1Library.Models;
using Milles_Project1Library.Services.CalculatorStrategyService;
using Milles_Project1Library.StrategyContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milles_Project1Library.Services
{
    public class CalculatorService : ICalculatorService
    {
        private readonly ProjectDbContext _dbContext;
        private readonly ICalculatorContext _calculatorContext;
        private ICalculatorStrategy _strategy;

        public CalculatorService(ILifetimeScope lifetimeScope)
        {
            _dbContext = lifetimeScope.Resolve<ProjectDbContext>();
            _calculatorContext = lifetimeScope.Resolve<ICalculatorContext>();
            _strategy = lifetimeScope.Resolve<ICalculatorStrategy>();

        }

        public void PerformCreateCalculation()
        {
            Console.Clear();
            Console.WriteLine("Choose an operation:");
            Console.WriteLine("1. (+) Addition");
            Console.WriteLine("2. (-) Subtraction");
            Console.WriteLine("3. (*) Multiplication");
            Console.WriteLine("4. (/) Division");
            Console.WriteLine("5. (√) Power of");
            Console.WriteLine("6. (%) Modulus");
            Console.WriteLine("Press 'e' to exit.\n");

            Console.Write("Enter your choice: ");
            string userInput = Console.ReadLine();

            if (userInput?.ToLower() == "e")
            {
                Console.WriteLine("Exiting calculation.");
                return;
            }

            if (int.TryParse(userInput, out int operationChoice))
            {
                if (operationChoice >= 1 && operationChoice <= 6)
                {
                    SetStrategyFromOperationChoice(operationChoice);

                    decimal num1 = _calculatorContext.GetUserInput("Enter the value for Number1 (max 1000000): ", 1, 1000000);
                    decimal num2 = _calculatorContext.GetUserInput("Enter the value for Number2 (max 1000000): ", 1, 1000000);

                    if (IsNumberOutOfRange(num1) || IsNumberOutOfRange(num2))
                    {
                        Message.ErrorMessage("Invalid input. Please enter numbers within a reasonable range.");
                        return;
                    }

                    num1 = Math.Round(num1, 2);
                    num2 = Math.Round(num2, 2);

                    decimal result = _calculatorContext.ExecuteOperation(num1, num2);

                    if (result == 0)
                    {
                        Console.ReadKey();
                        return;
                    }

                    result = Math.Round(result, 2);

                    Console.WriteLine($"Result: {result}");

                    _calculatorContext.SaveCalculationToDatabase(num1, num2, result);

                    Message.InputSuccessMessage("Calculation saved to the database successfully!");
                }
                else
                {
                    Message.ErrorMessage("Invalid operation choice. Please choose a valid operation.");
                }
            }
            else
            {
                Message.ErrorMessage("Invalid input. Please enter a number or 'e' to exit.");
            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        private bool IsNumberOutOfRange(decimal number)
        {
            const decimal MaxAllowedValue = 1000000;

            return Math.Abs(number) > MaxAllowedValue;
        }

        private void SetStrategyFromOperationChoice(int operationChoice)
        {
            switch (operationChoice)
            {
                case 1:
                    _strategy = new Addition();
                    break;
                case 2:
                    _strategy = new Subtraction();
                    break;
                case 3:
                    _strategy = new Multiplication();
                    break;
                case 4:
                    _strategy = new Division();
                    break;
                case 5:
                    _strategy = new PowerOf();
                    break;
                case 6:
                    _strategy = new Modulus();
                    break;
            }

            _calculatorContext.SetStrategy(_strategy);
        }

        public void ReadCalculation()
        {
            Console.Clear();
            var calculation = _dbContext.Calculator.ToList();

            Console.WriteLine("╭───────────────╮───────────────────────╮───────────────╮─────────────╮─────────────╮───────────────────╮");
            Console.WriteLine("│ Calculation ID| Operator              | Number 1      | Number 2    | Result      | Date              │");
            Console.WriteLine("├───────────────┼───────────────────────┼───────────────┼─────────────┼─────────────┼───────────────────┤");

            foreach (var c in calculation)
            {
                Console.WriteLine($"│{c.CalculationId,-15}│{c.Operator,-23}│{c.Number1,-15}│{c.Number2,-13}│{c.Result,-13}│{c.CalculationDate,-13}│");
                Console.WriteLine("├───────────────┼───────────────────────┼───────────────┼─────────────┼─────────────┼───────────────────┤");
            }

            Console.WriteLine("╰───────────────╯───────────────────────╯───────────────╯─────────────╯─────────────╯───────────────────╯");

            Console.WriteLine("Press any key to continue.");
        }

        public void UpdateCalculation()
        {
            Console.Write("Enter the Calculator ID you want to update: ");
            if (int.TryParse(Console.ReadLine(), out int calculatorId))
            {
                var calculation = _dbContext.Calculator.Find(calculatorId);

                if (calculation != null)
                {
                    Console.Write("Enter the new value for Number1: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal newNum1))
                    {
                        Console.Write("Enter the new value for Number2: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal newNum2))
                        {
                            newNum1 = Math.Round(newNum1, 2);
                            newNum2 = Math.Round(newNum2, 2);

                            UpdateCalculationInDatabase(calculation, newNum1, newNum2);
                            Message.InputSuccessMessage("Calculation updated successfully!");
                            return;
                        }
                        else
                        {
                            Message.ErrorMessage("Invalid input for Number2. Please enter a valid number.");
                        }
                    }
                    else
                    {
                        Message.ErrorMessage("Invalid input for Number1. Please enter a valid number.");
                    }
                }
                else
                {
                    Message.ErrorMessage("Calculation not found.");
                }
            }
            else
            {
                Message.ErrorMessage("Invalid input for Calculator ID. Please enter a valid number.");
            }

            Console.ReadKey();
        }

        public void DeleteCalculation()
        {
            Console.Write("Enter the Calculator ID you want to delete: ");
            if (int.TryParse(Console.ReadLine(), out int calculatorId))
            {
                var calculation = _dbContext.Calculator.Find(calculatorId);

                if (calculation != null)
                {
                    DeleteCalculationFromDatabase(calculation);
                    Message.InputSuccessMessage("Calculation deleted successfully!");
                    return;
                }
                else
                {
                    Message.ErrorMessage("Calculation not found.");
                }
            }
            else
            {
                Message.ErrorMessage("Invalid input for Calculator ID. Please enter a valid number.");
            }

            Console.ReadKey();
        }

        public void UpdateCalculationInDatabase(Calculator calculation, decimal newNum1, decimal newNum2)
        {
            newNum1 = Math.Round(newNum1, 2);
            newNum2 = Math.Round(newNum2, 2);

            if (IsNumberOutOfRange(newNum1) || IsNumberOutOfRange(newNum2))
            {
                Message.ErrorMessage("Invalid input. Please enter numbers within a reasonable range.");
                return;
            }

            calculation.Number1 = newNum1;
            calculation.Number2 = newNum2;
            decimal result = _calculatorContext.ExecuteOperation(newNum1, newNum2);

            if (IsResultOutOfRange(result))
            {
                Message.ErrorMessage("Result is too large. Please try again with smaller numbers.");
                return;
            }

            calculation.Result = Math.Round(result, 2);

            _calculatorContext.SaveCalculationToDatabase(newNum1, newNum2, calculation.Result);
        }

        private bool IsResultOutOfRange(decimal result)
        {
            const decimal MaxAllowedResult = 1000000;

            return Math.Abs(result) > MaxAllowedResult;
        }

        public void DeleteCalculationFromDatabase(Calculator calculation)
        {
            _dbContext.Calculator.Remove(calculation);
            _dbContext.SaveChanges();
        }
    }
}
