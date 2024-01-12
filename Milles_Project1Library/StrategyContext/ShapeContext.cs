using Autofac;
using Milles_Project1Library.Data;
using Milles_Project1Library.ExtraServices;
using Milles_Project1Library.Interfaces;
using Milles_Project1Library.Interfaces.ContextInterface;
using Milles_Project1Library.Interfaces.StrategyInterface;
using Milles_Project1Library.Models;

namespace Milles_Project1Library.StrategyContext
{
    public class ShapeContext : IShapeContext
    {
        private IShapeStrategy _shapeStrategy;
        private readonly ProjectDbContext _dbContext;

        public ShapeContext(ILifetimeScope lifetimeScope)
        {
            _dbContext = lifetimeScope.Resolve<ProjectDbContext>();
            _shapeStrategy = lifetimeScope.Resolve<IShapeStrategy>();
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

            if (area == 0 || perimeter == 0)
            {
                return;
            }
            Message.DarkYellowMessage($"\nArea: {area} cm");
            Message.DarkYellowMessage($"Perimeter: {perimeter} cm");

            SaveResultsToDatabase();
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

            Message.GreenMessage("\nShape calculation successfully saved to database!");
        }

        public void SetShapeProperties(decimal[] dimensions)
        {
            _shapeStrategy.SetDimensions(dimensions);
        }

        public decimal[] GetDimensionsInput()
        {
            if (_shapeStrategy is IShapeDimensionsProvider dimensionsProvider)
            {
                int dimensionCount = dimensionsProvider.GetDimensionCount();
                decimal[] dimensions = new decimal[dimensionCount];

                for (int i = 0; i < dimensionCount; i++)
                {
                    string dimensionName = GetDimensionName(i + 1);

                    Console.Write($"Enter {dimensionName} (1 - 1000) cm or 'e' to exit: ");
                    string userInput = Console.ReadLine();

                    if (userInput.ToLower() == "e")
                    {
                        dimensions[0] = 0;
                        dimensions[1] = 0;
                        return dimensions;
                    }
                    else if (decimal.TryParse(userInput, out decimal dimensionValue) && dimensionValue >= 1 && dimensionValue <= 1000)
                    {
                        dimensions[i] = dimensionValue;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid decimal between 1 and 999 or 'e' to exit.");
                        i--;
                    }
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
    }
}
