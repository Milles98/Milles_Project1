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

        public CalculatorService(ProjectDbContext dbContext, ICalculatorContext calculatorContext)
        {
            _dbContext = dbContext;
            _calculatorContext = calculatorContext;
        }

        public void PerformCreateCalculation()
        {
            Console.WriteLine("Choose an operation:");
            Console.WriteLine("1. (+) Addition");
            Console.WriteLine("2. (-) Subtraction");
            Console.WriteLine("3. (*) Multiplication");
            Console.WriteLine("4. (/) Division");
            Console.WriteLine("5. (√) Power of");
            Console.WriteLine("6. (%) Modulus");

            Console.Write("Enter your choice: ");
            if (int.TryParse(Console.ReadLine(), out int operationChoice))
            {
                if (operationChoice >= 1 && operationChoice <= 6)
                {
                    SetStrategyFromOperationChoice(operationChoice);

                    decimal num1 = _calculatorContext.GetUserInput("Enter the value for Number1: ");
                    decimal num2 = _calculatorContext.GetUserInput("Enter the value for Number2: ");

                    num1 = Math.Round(num1, 2);
                    num2 = Math.Round(num2, 2);

                    decimal result = _calculatorContext.ExecuteOperation(num1, num2);

                    result = Math.Round(result, 2);

                    Console.WriteLine($"Result: {result}");

                    _calculatorContext.SaveCalculationToDatabase(num1, num2, result);

                    Console.WriteLine("Calculation saved to database successfully!");
                }
                else
                {
                    Message.ErrorMessage("Invalid operation choice. Please choose a valid operation.");
                }
            }
            else
            {
                Message.ErrorMessage("Invalid input. Please enter a number.");
            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        private void SetStrategyFromOperationChoice(int operationChoice)
        {
            switch (operationChoice)
            {
                case 1:
                    _strategy = new AdditionStrategy();
                    break;
                case 2:
                    _strategy = new SubtractionStrategy();
                    break;
                case 3:
                    _strategy = new MultiplicationStrategy();
                    break;
                case 4:
                    _strategy = new DivisionStrategy();
                    break;
                case 5:
                    _strategy = new PowerOfStrategy();
                    break;
                case 6:
                    _strategy = new ModulusStrategy();
                    break;
            }

            _calculatorContext.SetStrategy(_strategy);
        }

        public void ReadCalculation()
        {
            Console.Clear();
            var calculation = _dbContext.Calculator.ToList();

            Console.WriteLine("╭───────────────╮───────────────────╮───────────────╮─────────────╮─────────────╮───────────────────╮");
            Console.WriteLine("│ Calculation ID| Operator          | Number 1      | Number 2    | Result      | Date              │");
            Console.WriteLine("├───────────────┼───────────────────┼───────────────┼─────────────┼─────────────┼───────────────────┤");

            foreach (var c in calculation)
            {
                Console.WriteLine($"│{c.CalculationId,-15}│{c.Operator,-19}│{c.Number1,-15}│{c.Number2,-13}│{c.Result,-13}│{c.CalculationDate,-13}│");
                Console.WriteLine("├───────────────┼───────────────────┼───────────────┼─────────────┼─────────────┼───────────────────┤");
            }

            Console.WriteLine("╰───────────────╯───────────────────╯───────────────╯─────────────╯─────────────╯───────────────────╯");
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
                            Console.WriteLine("Calculation updated successfully!");
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input for Number2. Please enter a valid number.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input for Number1. Please enter a valid number.");
                    }
                }
                else
                {
                    Console.WriteLine("Calculation not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input for Calculator ID. Please enter a valid number.");
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
                    Console.WriteLine("Calculation deleted successfully!");
                    return;
                }
                else
                {
                    Console.WriteLine("Calculation not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input for Calculator ID. Please enter a valid number.");
            }

            Console.ReadKey();
        }

        public void UpdateCalculationInDatabase(Calculator calculation, decimal newNum1, decimal newNum2)
        {
            calculation.Number1 = newNum1;
            calculation.Number2 = newNum2;
            calculation.Result = _calculatorContext.ExecuteOperation(newNum1, newNum2);

            calculation.Result = Math.Round(calculation.Result, 2);

            _calculatorContext.SaveCalculationToDatabase(newNum1, newNum2, calculation.Result);
        }

        public void DeleteCalculationFromDatabase(Calculator calculation)
        {
            _dbContext.Calculator.Remove(calculation);
            _dbContext.SaveChanges();
        }
    }
}
