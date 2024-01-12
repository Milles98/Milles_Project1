﻿using Autofac;
using Milles_Project1Library.Data;
using Milles_Project1Library.ExtraServices;
using Milles_Project1Library.Interfaces.ContextInterface;
using Milles_Project1Library.Interfaces.ServiceInterface;
using Milles_Project1Library.Interfaces.StrategyInterface;
using Milles_Project1Library.Models;
using Milles_Project1Library.Services.ShapeStrategyService;

namespace Milles_Project1Library.Services
{
    public class ShapeService : IShapeService
    {
        private readonly ProjectDbContext _dbContext;
        private readonly IShapeContext _shapeContext;
        //private readonly IShapeStrategy _shapeStrategy;

        public ShapeService(ILifetimeScope lifetimeScope)
        {
            _dbContext = lifetimeScope.Resolve<ProjectDbContext>();
            _shapeContext = lifetimeScope.Resolve<IShapeContext>();
            //_shapeStrategy = lifetimeScope.Resolve<IShapeStrategy>();
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

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("╭──────────────────────╮");
                Console.WriteLine("│Choose a shape type:  │");
                for (int i = 0; i < availableShapeTypes.Count(); i++)
                {
                    Console.WriteLine($"│{i + 1}. {availableShapeTypes.ElementAt(i),-19}│");
                }
                Console.WriteLine("╰──────────────────────╯");
                Console.ResetColor();

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
                    Message.RedMessage("Invalid input. Please enter a valid number or 'e' to exit.");
                    Console.ReadKey();
                }
            }
        }

        public void ReadShapes()
        {
            Console.Clear();
            var shape = _dbContext.Shape.ToList();

            Console.WriteLine("╭──────────╮──────────────╮───────────╮───────────╮────────────╮───────────────╮───────────╮───────────────────╮──────╮");
            Console.WriteLine("│ Shape ID | ShapeType    | Base      | Height    | SideLength | Area          │ Perimeter │ Date              │Active│");
            Console.WriteLine("├──────────┼──────────────┼───────────┼───────────┼────────────┼───────────────┤───────────┤───────────────────┤──────┤");

            foreach (var s in shape)
            {
                string sideLengthDisplay = s.SideLength.HasValue ? $"{s.SideLength.Value:F2} cm" : "N/A";
                string baseWithUnit = $"{s.Base:F2} cm";
                string heightWithUnit = $"{s.Height:F2} cm";
                string areaWithUnit = $"{s.Area:F2} cm²";
                string perimeterWithUnit = $"{s.Perimeter:F2} cm";
                if (s.IsActive)
                {

                    Console.WriteLine($"│{s.ShapeId,-10}│{s.ShapeType,-14}│{baseWithUnit,-11}│{heightWithUnit,-11}│" +
                        $"{sideLengthDisplay,-12}│{areaWithUnit,-15}│{perimeterWithUnit,-11}│{s.CalculationDate,-19}│{s.IsActive,-6}│");
                }
                else
                {
                    Message.RedMessage($"│{s.ShapeId,-10}│{s.ShapeType,-14}│{baseWithUnit,-11}│{heightWithUnit,-11}│" +
                                            $"{sideLengthDisplay,-12}│{areaWithUnit,-15}│{perimeterWithUnit,-11}│{s.CalculationDate,-19}│{s.IsActive,-6}│");
                }
                Console.WriteLine("├──────────┼──────────────┼───────────┼───────────┼────────────┼───────────────┤───────────┤───────────────────┤──────┤");
            }

            Console.WriteLine("╰──────────╯──────────────╯───────────╯───────────╯────────────╯───────────────╯───────────╯───────────────────╯──────╯");
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

                    if (shape != null && shape.IsActive)
                    {
                        Console.WriteLine($"Updating Shape ID: {shape.ShapeId}, Type: {shape.ShapeType}");

                        Console.Write($"Enter the new value for Base (1 - 1000) cm: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal newBase) && newBase >= 1 && newBase <= 1000)
                        {
                            newBase = Math.Round(newBase, 2);
                            shape.Base = newBase;
                        }
                        else
                        {
                            Message.RedMessage("Invalid input for Base. The Base remains unchanged. Please enter a value between 1 and 1000.");
                            continue;
                        }

                        Console.Write($"Enter the new value for Height (1 - 1000) cm: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal newHeight) && newHeight >= 1 && newHeight <= 1000)
                        {
                            newHeight = Math.Round(newHeight, 2);
                            shape.Height = newHeight;
                        }
                        else
                        {
                            Message.RedMessage("Invalid input for Height. The Height remains unchanged. Please enter a value between 1 and 1000.");
                            continue;
                        }

                        Console.Write($"Enter the new value for Side Length (1 - 1000) cm: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal newSideLength) && newSideLength >= 1 && newSideLength <= 1000)
                        {
                            newSideLength = Math.Round(newSideLength, 2);
                            shape.SideLength = newSideLength;
                        }
                        else
                        {
                            Message.RedMessage("Invalid input for Side Length. The Side Length remains unchanged. Please enter a value between 1 and 1000.");
                            continue;
                        }

                        SaveChangesToDatabase();

                        Message.GreenMessage("Shape updated successfully!");
                    }
                    else
                    {
                        Message.RedMessage("Shape not found or not active.");
                    }
                }
                else
                {
                    Message.RedMessage("Invalid input for Shape ID. Please enter a valid number or 'e' to exit.");
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
                Message.RedMessage($"Error saving changes to the database: {ex.Message}");
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

                    if (shape != null && shape.IsActive)
                    {
                        Console.WriteLine($"Deleting Shape ID: {shape.ShapeId}, Type: {shape.ShapeType}");

                        shape.IsActive = false;

                        Message.GreenMessage("Shape deleted successfully!");
                    }
                    else
                    {
                        Message.RedMessage("Shape not found or already inactive.");
                    }
                }
                else
                {
                    Message.RedMessage("Invalid input for Shape ID. Please enter a valid number or 'e' to exit.");
                }

                Console.ReadKey();
            }
        }
        public void ReActivateShape()
        {
            while (true)
            {
                Console.Clear();
                ReadShapes();

                Console.Write("Enter the Shape ID you want to reactivate or press 'e' to exit: ");
                string userInput = Console.ReadLine();

                if (userInput?.ToLower() == "e")
                {
                    Console.WriteLine("Exiting shape activation.");
                    return;
                }

                if (int.TryParse(userInput, out int shapeId))
                {
                    var shape = _dbContext.Shape.Find(shapeId);

                    if (shape != null && !shape.IsActive)
                    {
                        Console.WriteLine($"Activating Shape ID: {shape.ShapeId}, Type: {shape.ShapeType}");

                        shape.IsActive = true;

                        Message.GreenMessage("Shape activated successfully!");
                    }
                    else
                    {
                        Message.RedMessage("Shape not found or already active.");
                    }
                }
                else
                {
                    Message.RedMessage("Invalid input for Shape ID. Please enter a valid number or 'e' to exit.");
                }

                Console.ReadKey();
            }
        }
    }
}
