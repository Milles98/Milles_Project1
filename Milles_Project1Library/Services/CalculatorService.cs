using Autofac;
using Milles_Project1Library.Data;
using Milles_Project1Library.ExtraServices;
using Milles_Project1Library.Interfaces.ContextInterface;
using Milles_Project1Library.Interfaces.ServiceInterface;
using Milles_Project1Library.Interfaces.StrategyInterface;
using Milles_Project1Library.Models;
using Milles_Project1Library.Services.CalculatorStrategyService;

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
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("╭──────────────────────╮");
                Console.WriteLine("│1. (+) Addition       │");
                Console.WriteLine("│2. (-) Subtraction    │");
                Console.WriteLine("│3. (*) Multiplication │");
                Console.WriteLine("│4. (/) Division       │");
                Console.WriteLine("│5. (√) Square Root    │");
                Console.WriteLine("│6. (%) Modulus        │");
                Console.WriteLine("│Press 'e' to exit.    │");
                Console.WriteLine("╰──────────────────────╯");
                Console.ResetColor();

                Console.Write("\nEnter your choice: ");
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

                        if (operationChoice == 5)
                        {
                            decimal squareRoot = _calculatorContext.GetUserInput($"Enter the value for Number1 (max 1,000,000) or 'e' to exit: ", 1, 1000000);
                            squareRoot = Math.Round(squareRoot, 2);
                            decimal squareResult = _calculatorContext.ExecuteOperation(squareRoot, null);
                            squareResult = Math.Round(squareResult, 2);
                            if (squareResult == 0)
                            {
                                return;
                            }
                            Console.WriteLine($"Result: {squareResult}");
                            _calculatorContext.SaveCalculationToDatabase(squareRoot, null, squareResult);
                            Message.GreenMessage("Calculation saved to the database successfully!");
                            Console.ReadKey();
                            continue;
                        }

                        decimal num1 = _calculatorContext.GetUserInput($"Enter the value for Number1 (max 1,000,000) or 'e' to exit: ", 1, 1000000);

                        if (num1 == -1)
                        {
                            Console.WriteLine("Exiting calculation.");
                            return;
                        }

                        num1 = Math.Round(num1, 2);

                        decimal num2 = _calculatorContext.GetUserInput($"Enter the value for Number2 (max 1,000,000) or 'e' to exit: ", 1, 1000000);

                        if (num2 == -1)
                        {
                            Console.WriteLine("Exiting calculation.");
                            return;
                        }

                        num2 = Math.Round(num2, 2);

                        decimal result = _calculatorContext.ExecuteOperation(num1, num2);

                        if (result == 0)
                        {
                            Console.WriteLine("Press any key to go back to calculator menu.");
                            Console.ReadKey();
                            return;
                        }

                        result = Math.Round(result, 2);

                        Console.WriteLine($"Result: {result}");

                        _calculatorContext.SaveCalculationToDatabase(num1, num2, result);

                        Message.GreenMessage("Calculation saved to the database successfully!");

                        Console.ReadKey();
                    }
                    else
                    {
                        Message.RedMessage("Invalid operation choice. Please choose a valid operation.");
                    }
                }
                else
                {
                    Message.RedMessage("Invalid input. Please enter a number or 'e' to exit.");
                }
            }
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
                    _strategy = new SquareRoot();
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

            Console.WriteLine("╭───────────────╮───────────────────────╮───────────────╮─────────────╮─────────────╮───────────────────╮────────╮");
            Console.WriteLine("│ Calculation ID| Operator              | Number 1      | Number 2    | Result      | Date              │ Active │");
            Console.WriteLine("├───────────────┼───────────────────────┼───────────────┼─────────────┼─────────────┼───────────────────┤────────┤");

            foreach (var c in calculation)
            {
                string number2 = c.Number2.HasValue ? $"{c.Number2.Value:F2}" : "N/A";
                if (c.IsActive)
                {
                    Console.WriteLine($"│{c.CalculationId,-15}│{c.Operator,-23}│{c.Number1,-15:F2}│{number2,-13}│{c.Result,-13:F2}│{c.CalculationDate,-13}│{c.IsActive,-8}│");
                }
                else
                {
                    Message.RedMessage($"│{c.CalculationId,-15}│{c.Operator,-23}│{c.Number1,-15:F2}│{number2,-13}│{c.Result,-13:F2}│{c.CalculationDate,-13}│{c.IsActive,-8}│");
                }
                Console.WriteLine("├───────────────┼───────────────────────┼───────────────┼─────────────┼─────────────┼───────────────────┤────────┤");
            }

            Console.WriteLine("╰───────────────╯───────────────────────╯───────────────╯─────────────╯─────────────╯───────────────────╯────────╯");
        }

        public void UpdateCalculation()
        {
            while (true)
            {
                ReadCalculation();

                Console.Write("Enter the Calculator ID you want to update or press 'e' to exit: ");
                string userInput = Console.ReadLine();

                if (userInput?.ToLower() == "e")
                {
                    Console.WriteLine("Exiting update operation.");
                    break;
                }

                if (int.TryParse(userInput, out int calculatorId))
                {
                    var calculation = _dbContext.Calculator.Find(calculatorId);

                    if (calculation != null && calculation.IsActive)
                    {
                        if (_strategy is SquareRoot)
                        {
                            // For SquareRoot strategy, only one input is needed
                            Console.Write("Enter the new value for Number1 (1 - 1,000,000): ");
                            if (decimal.TryParse(Console.ReadLine(), out decimal newNum1) && newNum1 >= 1 && newNum1 <= 1000000)
                            {
                                newNum1 = Math.Round(newNum1, 2);

                                _calculatorContext.UpdateSquareRootCalculation(calculatorId, newNum1);

                                Message.GreenMessage("Calculation updated successfully!");
                                Console.WriteLine("Press any key to continue.");
                            }
                            else
                            {
                                Message.RedMessage("Invalid input for Number1. Please enter a valid number between 1 and 1,000,000.");
                            }
                        }
                        else
                        {
                            // For other strategies, ask for both inputs (Number1 and Number2)
                            Console.Write("Enter the new value for Number1 (1 - 1,000,000): ");
                            if (decimal.TryParse(Console.ReadLine(), out decimal newNum1) && newNum1 >= 1 && newNum1 <= 1000000)
                            {
                                Console.Write("Enter the new value for Number2 (1 - 1,000,000): ");
                                if (decimal.TryParse(Console.ReadLine(), out decimal newNum2) && newNum2 >= 1 && newNum2 <= 1000000)
                                {
                                    newNum2 = Math.Round(newNum2, 2);
                                    _calculatorContext.UpdateCalculation(calculatorId, newNum1, newNum2);

                                    Message.GreenMessage("Calculation updated successfully!");
                                    Console.WriteLine("Press any key to continue.");
                                }
                                else
                                {
                                    Message.RedMessage("Invalid input for Number2. Please enter a valid number between 1 and 1,000,000.");
                                }
                            }
                            else
                            {
                                Message.RedMessage("Invalid input for Number1. Please enter a valid number between 1 and 1,000,000.");
                            }
                        }
                    }
                    else
                    {
                        Message.RedMessage("Calculation not found or not active.");
                    }
                }
                else
                {
                    Message.RedMessage("Invalid input for Calculator ID. Please enter a valid number or 'e' to exit.");
                }

                Console.ReadKey();
            }
        }



        public void DeleteCalculation()
        {
            while (true)
            {
                ReadCalculation();
                Console.Write("Enter the Calculator ID you want to delete or press 'e' to exit: ");
                string userInput = Console.ReadLine();

                if (userInput?.ToLower() == "e")
                {
                    Console.WriteLine("Exiting calculator deletion.");
                    return;
                }
                if (int.TryParse(userInput, out int calculatorId))
                {
                    var calculation = _dbContext.Calculator.Find(calculatorId);

                    if (calculation != null && calculation.IsActive)
                    {
                        Console.WriteLine($"Deleting Calculator ID: {calculation.CalculationId}");

                        calculation.IsActive = false;

                        _dbContext.SaveChanges();

                        Message.GreenMessage("Calculation deleted successfully!");
                        Console.WriteLine("Press any key to continue.");
                    }
                    else
                    {
                        Message.RedMessage("Calculation not found or already inactive.");
                    }
                }
                else
                {
                    Message.RedMessage("Invalid input for Calculator ID. Please enter a valid number.");
                }

                Console.ReadKey();
            }
        }

        public void ReActivateCalculation()
        {
            while (true)
            {
                ReadCalculation();
                Console.Write("Enter the Calculator ID you want to activate or press 'e' to exit: ");
                string userInput = Console.ReadLine();

                if (userInput?.ToLower() == "e")
                {
                    Console.WriteLine("Exiting calculator activation.");
                    return;
                }
                if (int.TryParse(userInput, out int calculatorId))
                {
                    var calculation = _dbContext.Calculator.Find(calculatorId);

                    if (calculation != null && !calculation.IsActive)
                    {
                        Console.WriteLine($"Activating Calculator ID: {calculation.CalculationId}");

                        calculation.IsActive = true;

                        _dbContext.SaveChanges();

                        Message.GreenMessage("Calculation activated successfully!");
                        Console.WriteLine("Press any key to continue.");
                    }
                    else
                    {
                        Message.RedMessage("Calculation not found or already active.");
                    }
                }
                else
                {
                    Message.RedMessage("Invalid input for Calculator ID. Please enter a valid number.");
                }

                Console.ReadKey();
            }
        }

        public void UpdateCalculationInDatabase(Calculator calculation, decimal newNum1, decimal newNum2)
        {
            newNum1 = Math.Round(newNum1, 2);
            newNum2 = Math.Round(newNum2, 2);

            if (IsNumberOutOfRange(newNum1) || IsNumberOutOfRange(newNum2))
            {
                Message.RedMessage("Invalid input. Please enter numbers within a reasonable range.");
                return;
            }

            calculation.Number1 = newNum1;
            calculation.Number2 = newNum2;
            decimal result = _calculatorContext.ExecuteOperation(newNum1, newNum2);

            if (IsResultOutOfRange(result))
            {
                Message.RedMessage("Result is too large. Please try again with smaller numbers.");
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
    }
}
