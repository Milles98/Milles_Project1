using Milles_Project1Library.Data;
using Milles_Project1Library.ExtraServices;
using Milles_Project1Library.Interfaces.ContextInterface;
using Milles_Project1Library.Interfaces.StrategyInterface;
using Milles_Project1Library.Models;
using Milles_Project1Library.Services.CalculatorStrategyService;
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

        public decimal GetUserInput(string prompt)
        {
            decimal input;
            do
            {
                Console.Write(prompt);
            } while (!decimal.TryParse(Console.ReadLine(), out input));

            return input;
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

        public void CreateCalculation(decimal num1, decimal num2)
        {
            decimal result = ExecuteOperation(num1, num2);
            SaveCalculationToDatabase(num1, num2, result);
        }

        public IEnumerable<Calculator> ReadCalculation()
        {
            return _dbContext.Calculator.ToList();
        }

        public void UpdateCalculation(int calculationId, decimal num1, decimal num2)
        {
            var calculation = _dbContext.Calculator.Find(calculationId);

            if (calculation != null)
            {
                calculation.Number1 = num1;
                calculation.Number2 = num2;
                calculation.Result = ExecuteOperation(num1, num2);

                SaveCalculationToDatabase(num1, num2, calculation.Result);
            }
            else
            {
                Message.ErrorMessage("Calculation not found.");
            }
        }

        public void DeleteCalculation(int calculationId)
        {
            var calculation = _dbContext.Calculator.Find(calculationId);

            if (calculation != null)
            {
                _dbContext.Calculator.Remove(calculation);
                _dbContext.SaveChanges();
            }
            else
            {
                Message.ErrorMessage("Calculation not found.");
            }
        }


        public void SaveCalculationToDatabase(decimal num1, decimal num2, decimal result)
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
