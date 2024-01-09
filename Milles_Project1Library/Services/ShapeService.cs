using Milles_Project1Library.Data;
using Milles_Project1Library.ExtraServices;
using Milles_Project1Library.Interfaces.ContextInterface;
using Milles_Project1Library.Interfaces.ServiceInterface;
using Milles_Project1Library.Interfaces.StrategyInterface;
using Milles_Project1Library.Models;
using Milles_Project1Library.Services.ShapeStrategyService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milles_Project1Library.Services
{
    public class ShapeService : IShapeService
    {
        private readonly ProjectDbContext _dbContext;
        private readonly IShapeContext _shapeContext;
        private readonly IShapeStrategy _shapeStrategy;

        public ShapeService(ProjectDbContext dbContext, IShapeContext shapeContext, IShapeStrategy shapeStrategy)
        {
            _dbContext = dbContext;
            _shapeContext = shapeContext;
            _shapeStrategy = shapeStrategy;
        }

        public IEnumerable<string> GetAvailableShapeTypes()
        {
            return new List<string> { "Parallelogram", "Rectangle", "Rhombus", "Triangle" };
        }

        private IShapeStrategy GetShapeStrategy(string shapeType)
        {
            switch (shapeType.ToLower())
            {
                case "parallelogram":
                    return new ParallelogramStrategy();
                case "rectangle":
                    return new RectangleStrategy();
                case "rhombus":
                    return new RhombusStrategy();
                case "triangle":
                    return new TriangleStrategy();
                default:
                    throw new ArgumentException($"Unsupported shape type: {shapeType}");
            }
        }

        public void CreateShape()
        {
            Console.Clear();

            var availableShapeTypes = GetAvailableShapeTypes();

            Console.WriteLine("Choose a shape type:");

            for (int i = 0; i < availableShapeTypes.Count(); i++)
            {
                Console.WriteLine($"{i + 1}. {availableShapeTypes.ElementAt(i)}");
            }

            int shapeTypeChoice;
            do
            {
                Console.Write("Enter the number corresponding to the shape type: ");
            } while (!int.TryParse(Console.ReadLine(), out shapeTypeChoice) || shapeTypeChoice < 1 || shapeTypeChoice > availableShapeTypes.Count());

            string selectedShapeType = availableShapeTypes.ElementAt(shapeTypeChoice - 1);

            _shapeContext.SetShapeCalculator(GetShapeStrategy(selectedShapeType));

            _shapeContext.CalculateAndDisplayResults();

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        public void ReadShapes()
        {
            Console.Clear();
            var shape = _dbContext.Shape.ToList();

            Console.WriteLine("╭──────────╮──────────────╮───────────────╮─────────────╮─────────────╮───────────╮───────────╮───────────────────╮");
            Console.WriteLine("│ Shape ID | ShapeType    | Base          | Height      | SideLength  | Area      │ Perimeter │ Date              │");
            Console.WriteLine("├──────────┼──────────────┼───────────────┼─────────────┼─────────────┼───────────┤───────────┤───────────────────┤");

            foreach (var s in shape)
            {
                string sideLengthDisplay = s.SideLength == 0 ? "N/A" : $"{s.SideLength:F2} cm";
                string baseWithUnit = $"{s.Base} cm";
                string heightWithUnit = $"{s.Height} cm";
                string areaWithUnit = $"{s.Area} cm²";
                string perimeterWithUnit = $"{s.Perimeter} cm";

                Console.WriteLine($"│{s.ShapeId,-10}│{s.ShapeType,-14}│{baseWithUnit,-15}│{heightWithUnit,-13}│" +
                    $"{sideLengthDisplay,-13}│{areaWithUnit,-11}│{perimeterWithUnit,-11}│{s.CalculationDate,-19}│");
                Console.WriteLine("├──────────┼──────────────┼───────────────┼─────────────┼─────────────┼───────────┤───────────┤───────────────────┤");
            }

            Console.WriteLine("╰──────────╯──────────────╯───────────────╯─────────────╯─────────────╯───────────╯───────────╯───────────────────╯");
        }

        public void UpdateShape()
        {
            ReadShapes();

            Console.Write("\nEnter the Shape ID you want to update: ");
            if (int.TryParse(Console.ReadLine(), out int shapeId))
            {
                var shape = _dbContext.Shape.Find(shapeId);

                if (shape != null)
                {
                    Console.WriteLine($"Updating Shape ID: {shape.ShapeId}, Type: {shape.ShapeType}");

                    Console.Write("Enter the new value for Base: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal newBase))
                    {
                        newBase = Math.Round(newBase, 2);

                        shape.Base = newBase;
                    }
                    else
                    {
                        Message.ErrorMessage("Invalid input for Base. The Base remains unchanged.");
                    }

                    Console.Write("Enter the new value for Height: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal newHeight))
                    {
                        newHeight = Math.Round(newHeight, 2);

                        shape.Height = newHeight;
                    }
                    else
                    {
                        Message.ErrorMessage("Invalid input for Height. The Height remains unchanged.");
                    }

                    Console.Write("Enter the new value for Side Length: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal newSideLength))
                    {
                        newSideLength = Math.Round(newSideLength, 2);

                        shape.SideLength = newSideLength;
                    }
                    else
                    {
                        Message.ErrorMessage("Invalid input for Side Length. The Side Length remains unchanged.");
                    }

                    SaveResultsToDatabase(shape);

                    Message.InputSuccessMessage("Shape updated successfully!");
                }
                else
                {
                    Message.ErrorMessage("Shape not found.");
                }
            }
            else
            {
                Message.ErrorMessage("Invalid input for Shape ID. Please enter a valid number.");
            }

            Console.ReadKey();
        }

        public void DeleteShape()
        {
            Console.Clear();
            ReadShapes();
            Console.Write("Enter the Shape ID you want to delete: ");

            if (int.TryParse(Console.ReadLine(), out int shapeId))
            {
                var shape = _dbContext.Shape.Find(shapeId);

                if (shape != null)
                {
                    Console.WriteLine($"Deleting Shape ID: {shape.ShapeId}, Type: {shape.ShapeType}");

                    DeleteShapeFromDatabase(shape);

                    Message.InputSuccessMessage("Shape deleted successfully!");
                }
                else
                {
                    Message.ErrorMessage("Shape not found.");
                }
            }
            else
            {
                Message.ErrorMessage("Invalid input for Shape ID. Please enter a valid number.");
            }

            Console.ReadKey();
        }

        public void SaveResultsToDatabase(Shape resultShape)
        {
            if (_shapeStrategy == null)
            {
                Message.ErrorMessage("No shape calculator selected.");
                return;
            }

            _dbContext.Shape.Add(resultShape);
            _dbContext.SaveChanges();
        }

        private void DeleteShapeFromDatabase(Shape shape)
        {
            _dbContext.Shape.Remove(shape);
            _dbContext.SaveChanges();
        }
    }
}
