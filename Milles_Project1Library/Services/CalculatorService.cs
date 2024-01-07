using Microsoft.EntityFrameworkCore;
using Milles_Project1Library.Data;
using Milles_Project1Library.Interfaces;
using Milles_Project1Library.Models;
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

        public CalculatorService(ProjectDbContext dbContext, ICalculatorContext calculatorContext)
        {
            _dbContext = dbContext;
            _calculatorContext = calculatorContext;
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

            _calculatorContext.SaveCalculationToDatabase(newNum1, newNum2, calculation.Result);
        }

        public void DeleteCalculationFromDatabase(Calculator calculation)
        {
            _dbContext.Calculator.Remove(calculation);
            _dbContext.SaveChanges();
        }
    }
}
