using Microsoft.EntityFrameworkCore;
using Milles_Project1Library.Data;
using Milles_Project1Library.ExtraServices;
using Milles_Project1Library.Interfaces;
using Milles_Project1Library.Interfaces.ContextInterface;
using Milles_Project1Library.Interfaces.StrategyInterface;
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
        private readonly ProjectDbContext _dbContext;

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

            decimal[] dimensions = GetDimensionsInput();

            SetShapeProperties(dimensions);

            decimal area = _shapeStrategy.CalculateArea();
            decimal perimeter = _shapeStrategy.CalculatePerimeter();

            area = Math.Round(area, 2);
            perimeter = Math.Round(perimeter, 2);

            Message.DarkYellowMessage($"\nArea: {area} cm");
            Message.DarkYellowMessage($"Perimeter: {perimeter} cm");

            SaveResultsToDatabase();
            SaveResultsToUserHistory(area, perimeter);
        }

        private void SaveResultsToDatabase()
        {
            if (_shapeStrategy == null)
            {
                Console.WriteLine("No shape calculator selected.");
                return;
            }

            string shapeType = _shapeStrategy.ShapeType;

            decimal area = _shapeStrategy.CalculateArea();
            decimal perimeter = _shapeStrategy.CalculatePerimeter();

            area = Math.Round(area, 2);
            perimeter = Math.Round(perimeter, 2);

            var resultShape = new Shape
            {
                ShapeType = shapeType,
                Base = _shapeStrategy.Base,
                Height = _shapeStrategy.Height,
                SideLength = _shapeStrategy.SideLength,
                Area = area,
                Perimeter = perimeter,
                CalculationDate = DateTime.Now
            };

            _dbContext.Shape.Add(resultShape);
            _dbContext.SaveChanges();
        }

        private void SaveResultsToUserHistory(decimal area, decimal perimeter)
        {
            if (_shapeStrategy == null)
            {
                Console.WriteLine("No shape calculator selected.");
                return;
            }

            string shapeType = _shapeStrategy.ShapeType;

            area = Math.Round(area, 2);
            perimeter = Math.Round(perimeter, 2);

            var userHistory = new UserHistory
            {
                ActionType = "Shapes",
                Action = "C", // Exempel: C, R, U, D
                DatePerformed = DateTime.Now,
                Description = $"ShapeType: {shapeType}, Area: {area}, Perimeter: {perimeter}"
            };

            _dbContext.UserHistory.Add(userHistory);
            _dbContext.SaveChanges();
        }

        private void SetShapeProperties(decimal[] dimensions)
        {
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
                    string dimensionName = GetDimensionName(i + 1);
                    dimensions[i] = GetDoubleInput($"Enter {dimensionName} (cm): ");
                }

                return dimensions;
            }

            return Array.Empty<decimal>();
        }

        private string GetDimensionName(int dimensionIndex)
        {
            switch (dimensionIndex)
            {
                case 1:
                    return "Base";
                case 2:
                    return "Height";
                case 3:
                    return "SideLength";
                default:
                    return "Dimension";
            }
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
