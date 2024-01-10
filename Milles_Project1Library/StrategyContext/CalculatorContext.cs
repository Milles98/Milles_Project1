using Autofac;
using Milles_Project1Library.Data;
using Milles_Project1Library.ExtraServices;
using Milles_Project1Library.Interfaces.ContextInterface;
using Milles_Project1Library.Interfaces.StrategyInterface;
using Milles_Project1Library.Models;

namespace Milles_Project1Library.StrategyContext
{
    public class CalculatorContext : ICalculatorContext
    {
        private ICalculatorStrategy _strategy;
        private readonly ProjectDbContext _dbContext;

        public CalculatorContext(ILifetimeScope lifetimeScope)
        {
            _dbContext = lifetimeScope.Resolve<ProjectDbContext>();
            _strategy = lifetimeScope.Resolve<ICalculatorStrategy>();
        }

        public decimal GetUserInput(string prompt, decimal minValue, decimal maxValue)
        {
            return GetBoundedDoubleInput(prompt, minValue, maxValue);
        }

        private decimal GetBoundedDoubleInput(string prompt, decimal minValue, decimal maxValue)
        {
            decimal input = 0;
            bool isValidInput = false;
            string userInput = "";

            do
            {
                Console.Write(prompt);
                userInput = Console.ReadLine();

                if (userInput.ToLower() == "e")
                {
                    Console.WriteLine("Exiting calculation.");
                    return -1;
                }

                if (!decimal.TryParse(userInput, out input))
                {
                    Message.ErrorMessage("Invalid input. Please enter a valid number.");
                    continue;
                }

                isValidInput = input >= minValue && input <= maxValue;

                if (!isValidInput)
                {
                    Message.ErrorMessage($"Invalid input. Please enter a value between {minValue} and {maxValue} or 'e' to exit.");
                }
            } while (!isValidInput);

            return Math.Round(input, 2);
        }

        public decimal ExecuteOperation(decimal num1, decimal num2)
        {
            decimal result = _strategy.Calculate(num1, num2);

            return Math.Round(result, 2);
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
            SaveCalculationDetails(num1, num2, result);

        }

        private void SaveCalculationDetails(decimal num1, decimal num2, decimal result)
        {
            var calculation = new Calculator
            {
                Number1 = num1,
                Number2 = num2,
                Result = result,
                CalculationDate = DateTime.Now,
                Operator = _strategy.GetType().Name
            };

            _dbContext.Calculator.Add(calculation);
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
