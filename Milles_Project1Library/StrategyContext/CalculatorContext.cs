using Autofac;
using Milles_Project1Library.Data;
using Milles_Project1Library.ExtraServices;
using Milles_Project1Library.Interfaces.ContextInterface;
using Milles_Project1Library.Interfaces.StrategyInterface;
using Milles_Project1Library.Models;
using Milles_Project1Library.Services.CalculatorStrategyService;

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
                    Message.RedMessage("Invalid input. Please enter a valid number.");
                    continue;
                }

                isValidInput = input >= minValue && input <= maxValue;

                if (!isValidInput)
                {
                    Message.RedMessage($"Invalid input. Please enter a value between {minValue} and {maxValue} or 'e' to exit.");
                }
            } while (!isValidInput);

            return Math.Round(input, 2);
        }

        public decimal ExecuteOperation(decimal num1, decimal? num2)
        {
            decimal result = _strategy.Calculate(num1, num2);

            return Math.Round(result, 2);
        }

        public void SetStrategy(ICalculatorStrategy strategy)
        {
            _strategy = strategy;
        }

        public void CreateCalculation(decimal num1, decimal? num2)
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
            var calculation = GetCalculationById(calculationId);

            if (calculation != null)
            {
                calculation.Number1 = num1;
                calculation.Number2 = num2;
                calculation.Result = ExecuteOperation(num1, num2);

                _dbContext.SaveChanges();
            }
            else
            {
                Message.RedMessage("Calculation not found.");
            }
        }

        public void UpdateSquareRootCalculation(int calculationId, decimal num1)
        {
            var calculation = GetCalculationById(calculationId);

            if (calculation != null)
            {
                calculation.Number1 = num1;
                calculation.Number2 = null;

                SetStrategy(new SquareRoot());

                calculation.Result = ExecuteOperation(num1, null);

                _dbContext.SaveChanges();
            }
            else
            {
                Message.RedMessage("Calculation not found.");
            }
        }

        public void SaveCalculationToDatabase(decimal num1, decimal? num2, decimal? result)
        {
            SaveCalculationDetails(num1, num2, result);

        }

        private void SaveCalculationDetails(decimal num1, decimal? num2, decimal? result)
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
        private Calculator GetCalculationById(int calculationId)
        {
            return _dbContext.Calculator.Find(calculationId);
        }
    }
}
