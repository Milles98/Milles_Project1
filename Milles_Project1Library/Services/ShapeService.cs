using Autofac;
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

        public ShapeService(ILifetimeScope lifetimeScope)
        {
            _dbContext = lifetimeScope.Resolve<ProjectDbContext>();
            _shapeContext = lifetimeScope.Resolve<IShapeContext>();
            _shapeStrategy = lifetimeScope.Resolve<IShapeStrategy>();
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
                    return new Parallelogram();
                case "rectangle":
                    return new Rectangle();
                case "rhombus":
                    return new Rhombus();
                case "triangle":
                    return new Triangle();
                default:
                    throw new ArgumentException($"Unsupported shape type: {shapeType}");
            }
        }

        public void CreateShape()
        {
            while (true)
            {
                Console.Clear();

                var availableShapeTypes = GetAvailableShapeTypes();

                Console.WriteLine("Choose a shape type:");

                for (int i = 0; i < availableShapeTypes.Count(); i++)
                {
                    Console.WriteLine($"{i + 1}. {availableShapeTypes.ElementAt(i)}");
                }

                Console.Write("Enter the number corresponding to the shape type or press 'e' to exit: ");
                string userInput = Console.ReadLine();

                if (userInput?.ToLower() == "e")
                {
                    Console.WriteLine("Exiting shape creation.");
                    return;
                }

                if (int.TryParse(userInput, out int shapeTypeChoice) && shapeTypeChoice >= 1 && shapeTypeChoice <= availableShapeTypes.Count())
                {
                    string selectedShapeType = availableShapeTypes.ElementAt(shapeTypeChoice - 1);

                    _shapeContext.SetShapeCalculator(GetShapeStrategy(selectedShapeType));

                    _shapeContext.CalculateAndDisplayResults();

                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                }
                else
                {
                    Message.ErrorMessage("Invalid input. Please enter a valid number or 'e' to exit.");
                    Console.ReadKey();
                }
            }
        }

        public void ReadShapes()
        {
            Console.Clear();
            var shape = _dbContext.Shape.ToList();

            Console.WriteLine("╭──────────╮──────────────╮─────────────╮─────────────╮─────────────╮─────────────╮───────────╮───────────────────╮");
            Console.WriteLine("│ Shape ID | ShapeType    | Base        | Height      | SideLength  | Area        │ Perimeter │ Date              │");
            Console.WriteLine("├──────────┼──────────────┼─────────────┼─────────────┼─────────────┼─────────────┤───────────┤───────────────────┤");

            foreach (var s in shape)
            {
                string sideLengthDisplay = s.SideLength == 0 ? "N/A" : $"{s.SideLength:F2} cm";
                string baseWithUnit = $"{s.Base} cm";
                string heightWithUnit = $"{s.Height} cm";
                string areaWithUnit = $"{s.Area} cm²";
                string perimeterWithUnit = $"{s.Perimeter} cm";

                Console.WriteLine($"│{s.ShapeId,-10}│{s.ShapeType,-14}│{baseWithUnit,-13}│{heightWithUnit,-13}│" +
                    $"{sideLengthDisplay,-13}│{areaWithUnit,-13}│{perimeterWithUnit,-11}│{s.CalculationDate,-19}│");
                Console.WriteLine("├──────────┼──────────────┼─────────────┼─────────────┼─────────────┼─────────────┤───────────┤───────────────────┤");
            }

            Console.WriteLine("╰──────────╯──────────────╯─────────────╯─────────────╯─────────────╯─────────────╯───────────╯───────────────────╯");

            Console.WriteLine("Press any key to continue.");
        }

        public void UpdateShape()
        {
            while (true)
            {
                ReadShapes();

                Console.Write("\nEnter the Shape ID you want to update or press 'e' to exit: ");
                string userInput = Console.ReadLine();

                if (userInput?.ToLower() == "e")
                {
                    Console.WriteLine("Exiting update operation.");
                    return;
                }

                if (int.TryParse(userInput, out int shapeId))
                {
                    var shape = _dbContext.Shape.Find(shapeId);

                    if (shape != null)
                    {
                        Console.WriteLine($"Updating Shape ID: {shape.ShapeId}, Type: {shape.ShapeType}");

                        Console.Write($"Enter the new value for Base (1-999) cm: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal newBase) && newBase >= 1 && newBase <= 999)
                        {
                            newBase = Math.Round(newBase, 2);
                            shape.Base = newBase;
                        }
                        else
                        {
                            Message.ErrorMessage("Invalid input for Base. The Base remains unchanged. Please enter a value between 1 and 999.");
                            continue;
                        }

                        Console.Write($"Enter the new value for Height (1-999) cm: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal newHeight) && newHeight >= 1 && newHeight <= 999)
                        {
                            newHeight = Math.Round(newHeight, 2);
                            shape.Height = newHeight;
                        }
                        else
                        {
                            Message.ErrorMessage("Invalid input for Height. The Height remains unchanged. Please enter a value between 1 and 999.");
                            continue;
                        }

                        Console.Write($"Enter the new value for Side Length (1-999) cm: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal newSideLength) && newSideLength >= 1 && newSideLength <= 999)
                        {
                            newSideLength = Math.Round(newSideLength, 2);
                            shape.SideLength = newSideLength;
                        }
                        else
                        {
                            Message.ErrorMessage("Invalid input for Side Length. The Side Length remains unchanged. Please enter a value between 1 and 999.");
                            continue;
                        }

                        SaveChangesToDatabase();

                        Message.InputSuccessMessage("Shape updated successfully!");
                    }
                    else
                    {
                        Message.ErrorMessage("Shape not found.");
                    }
                }
                else
                {
                    Message.ErrorMessage("Invalid input for Shape ID. Please enter a valid number or 'e' to exit.");
                }

                Console.ReadKey();
            }
        }


        private void SaveChangesToDatabase()
        {
            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Message.ErrorMessage($"Error saving changes to the database: {ex.Message}");
            }
        }

        public void DeleteShape()
        {
            while (true)
            {
                Console.Clear();
                ReadShapes();

                Console.Write("Enter the Shape ID you want to delete or press 'e' to exit: ");
                string userInput = Console.ReadLine();

                if (userInput?.ToLower() == "e")
                {
                    Console.WriteLine("Exiting shape deletion.");
                    return;
                }

                if (int.TryParse(userInput, out int shapeId))
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
                    Message.ErrorMessage("Invalid input for Shape ID. Please enter a valid number or 'e' to exit.");
                }

                Console.ReadKey();
            }
        }

        private void DeleteShapeFromDatabase(Shape shape)
        {
            _dbContext.Shape.Remove(shape);
            _dbContext.SaveChanges();
        }
    }
}
