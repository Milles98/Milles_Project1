using Autofac;
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

        public ShapeService(ILifetimeScope lifetimeScope)
        {
            _dbContext = lifetimeScope.Resolve<ProjectDbContext>();
            _shapeContext = lifetimeScope.Resolve<IShapeContext>();
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
                }
                else
                {
                    Message.RedMessage("Invalid input. Please enter a valid number or 'e' to exit.");
                    Console.ReadKey();
                }
            }
        }

        public void ReadShape()
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
                ReadShape();

                Console.Write("Enter the ID of the shape you want to update (press 'e' to cancel): ");
                string shapeIdInput = Console.ReadLine();

                if (shapeIdInput.ToLower() == "e")
                {
                    Message.DarkYellowMessage("Update canceled.");
                    return;
                }

                if (int.TryParse(shapeIdInput, out int shapeId))
                {
                    var shapeToUpdate = _dbContext.Shape.Find(shapeId);

                    if (shapeToUpdate == null || !shapeToUpdate.IsActive)
                    {
                        Message.RedMessage($"Shape with ID {shapeId} not found or inactive.");
                        Console.ReadKey();
                        return;
                    }

                    var shapeType = shapeToUpdate.ShapeType;
                    var shapeStrategy = GetShapeStrategy(shapeType);
                    _shapeContext.SetShapeCalculator(shapeStrategy);

                    Console.WriteLine($"Enter new dimensions for the {shapeType} (press 'e' to cancel):");

                    decimal[] dimensions = _shapeContext.GetDimensionsInput();

                    if (dimensions[0] == 0 && dimensions[1] == 0)
                    {
                        Message.DarkYellowMessage($"Update for shape with ID {shapeId} canceled.");
                        return;
                    }

                    _shapeContext.SetShapeProperties(dimensions);

                    decimal area = shapeStrategy.CalculateArea();
                    decimal perimeter = shapeStrategy.CalculatePerimeter();

                    area = Math.Round(area, 2);
                    perimeter = Math.Round(perimeter, 2);

                    shapeToUpdate.Base = shapeStrategy.Base;
                    shapeToUpdate.Height = shapeStrategy.Height;
                    shapeToUpdate.SideLength = shapeStrategy.SideLength;
                    shapeToUpdate.Area = area;
                    shapeToUpdate.Perimeter = perimeter;

                    _dbContext.SaveChanges();

                    Message.GreenMessage($"Shape with ID {shapeId} successfully updated!");
                    Console.ReadKey();
                }
                else
                {
                    Message.RedMessage("Invalid input. Please enter a valid shape ID.");
                }
            }
        }

        public void DeleteShape()
        {
            while (true)
            {
                Console.Clear();
                ReadShape();

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

                        _dbContext.SaveChanges();

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
                ReadShape();

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

    }
}
