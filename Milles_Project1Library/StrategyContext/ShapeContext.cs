using Microsoft.EntityFrameworkCore;
using Milles_Project1Library.Data;
using Milles_Project1Library.Interfaces;
using Milles_Project1Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milles_Project1Library.StrategyContext
{
    public class ShapeContext : IShapeContext
    {
        private IShapeStrategy _shapeStrategy;
        private readonly ProjectDbContext _dbContext; // Lägg till din DbContext här

        public ShapeContext(ProjectDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public void SetShapeCalculator(IShapeStrategy shapeStrategy)
        {
            _shapeStrategy = shapeStrategy;
        }

        public void CalculateAndDisplayResults()
        {
            if (_shapeStrategy == null)
            {
                Console.WriteLine("No shape calculator selected.");
                return;
            }

            // Assume you have settings or input for the shape, e.g., side lengths, etc.
            decimal[] dimensions = GetDimensionsInput();

            // Set values on _shapeStrategy based on user input
            SetShapeProperties(dimensions);

            // Calculate and display the results
            decimal area = _shapeStrategy.CalculateArea();
            decimal perimeter = _shapeStrategy.CalculatePerimeter();

            Console.WriteLine($"Area: {area}");
            Console.WriteLine($"Perimeter: {perimeter}");

            SaveResultsToDatabase();
            SaveResultsToUserHistory(area, perimeter);

            Console.ReadKey();
        }

        private void SaveResultsToDatabase()
        {
            // Kontrollera först om _shapeStrategy är satt
            if (_shapeStrategy == null)
            {
                Console.WriteLine("No shape calculator selected.");
                return;
            }

            // Få typen av form från den aktuella strategin
            string shapeType = _shapeStrategy.ShapeType;

            // Anta att dessa metoder är tillgängliga på din _shapeStrategy
            decimal area = _shapeStrategy.CalculateArea();
            decimal perimeter = _shapeStrategy.CalculatePerimeter();

            // Skapa en ny Shape-instans med resultaten
            var resultShape = new Shape
            {
                ShapeType = shapeType,
                Area = area,
                Perimeter = perimeter,
                CalculationDate = DateTime.Now
            };

            // Spara i databasen
            _dbContext.Shape.Add(resultShape);
            _dbContext.SaveChanges();
        }

        private void SaveResultsToUserHistory(decimal area, decimal perimeter)
        {
            // Kontrollera först om _shapeStrategy är satt
            if (_shapeStrategy == null)
            {
                Console.WriteLine("No shape calculator selected.");
                return;
            }

            // Få typen av form från den aktuella strategin
            string shapeType = _shapeStrategy.ShapeType;

            // Skapa en ny UserHistory-instans med resultaten
            var userHistory = new UserHistory
            {
                ActionType = "Shapes",
                Action = "C", // Exempel: C, R, U, D
                DatePerformed = DateTime.Now,
                Description = $"ShapeType: {shapeType}, Area: {area}, Perimeter: {perimeter}"
            };

            // Spara i databasen
            _dbContext.UserHistory.Add(userHistory);
            _dbContext.SaveChanges();
        }

        private void SetShapeProperties(decimal[] dimensions)
        {
            // Assume you set user input values on _shapeStrategy
            // This can vary depending on the type of shape you're working with
            // For example, you might have methods like SetSideLength, SetHeight, etc. in your IShapeStrategy interface
            _shapeStrategy.SetDimensions(dimensions);
        }

        private decimal[] GetDimensionsInput()
        {
            if (_shapeStrategy is IShapeDimensionsProvider dimensionsProvider)
            {
                int dimensionCount = dimensionsProvider.GetDimensionCount();
                decimal[] dimensions = new decimal[dimensionCount];

                for (int i = 0; i < dimensionCount; i++)
                {
                    dimensions[i] = GetDoubleInput($"Enter dimension {i + 1}:");
                }

                return dimensions;
            }

            return Array.Empty<decimal>();
        }

        private decimal GetDoubleInput(string prompt)
        {
            decimal result;
            bool validInput;

            do
            {
                Console.Write(prompt);
                validInput = decimal.TryParse(Console.ReadLine(), out result);

                if (!validInput)
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }

            } while (!validInput);

            return result;
        }
    }
}
