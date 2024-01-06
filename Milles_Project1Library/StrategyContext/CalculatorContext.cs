using Milles_Project1Library.Data;
using Milles_Project1Library.ExtraServices;
using Milles_Project1Library.Interfaces;
using Milles_Project1Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milles_Project1Library.StrategyContext
{
    public class CalculatorContext : ICalculatorContext
    {
        private ICalculatorStrategy _strategy;
        private readonly ProjectDbContext _dbContext;

        public CalculatorContext(ICalculatorStrategy strategy, ProjectDbContext dbContext)
        {
            _strategy = strategy;
            _dbContext = dbContext;
        }

        public decimal ExecuteOperation(decimal num1, decimal num2)
        {
            decimal result = _strategy.Calculate(num1, num2);

            return result;
        }

        public void SetStrategy(ICalculatorStrategy strategy)
        {
            _strategy = strategy;
        }

        private void SaveCalculationToDatabase(decimal num1, decimal num2, decimal result)
        {
            // Save calculation details
            SaveCalculationDetails(num1, num2, result);

            // Save calculation history
            SaveCalculationHistory(result);
        }

        private void SaveCalculationDetails(decimal num1, decimal num2, decimal result)
        {
            // Create a new instance of Calculator
            var calculation = new Calculator
            {
                Number1 = num1,
                Number2 = num2,
                Result = result,
                CalculationDate = DateTime.Now,
                Operator = _strategy.GetType().Name // Assuming _strategy is set elsewhere in your code
            };

            // Save in the database
            _dbContext.Calculator.Add(calculation);
            _dbContext.SaveChanges();
        }

        private void SaveCalculationHistory(decimal result)
        {
            // Create a new instance of CalculationHistory
            var calculationHistory = new UserHistory
            {
                ActionType = "Calculator",
                Action = "C", // Exempel: C, R, U, D
                DatePerformed = DateTime.Now,
                Description = $"Operation: {_strategy.GetType().Name}, Result: {result}"
            };

            // Save in the database
            _dbContext.UserHistory.Add(calculationHistory);
            _dbContext.SaveChanges();
        }

        public void CalculateAndDisplayResults()
        {
            Console.Write("Enter the first number: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal num1))
            {
                Console.Write("Enter the second number: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal num2))
                {
                    decimal result = ExecuteOperation(num1, num2);
                    Console.WriteLine($"Result: {result}");
                    SaveCalculationToDatabase(num1, num2, result);
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
