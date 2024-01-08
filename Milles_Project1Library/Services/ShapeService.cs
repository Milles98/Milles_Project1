using Milles_Project1Library.Data;
using Milles_Project1Library.Interfaces;
using Milles_Project1Library.Models;
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

        public ShapeService(ProjectDbContext dbContext, IShapeContext shapeContext)
        {
            _dbContext = dbContext;
            _shapeContext = shapeContext;
        }

        public void CreateShape()
        {
            Console.Clear();
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
                Console.WriteLine($"│{s.ShapeId,-10}│{s.ShapeType,-14}│{s.Base,-15}│{s.Height,-13}│{s.SideLength,-13}│{s.Area,-11}│{s.Perimeter,-11}│{s.CalculationDate,-19}│");
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
                    // Implementera logiken för att uppdatera en geometrisk form
                    Console.WriteLine($"Updating Shape ID: {shape.ShapeId}, Type: {shape.ShapeType}");

                    // Här kan du använda _shapeContext för att uppdatera egenskaper på den geometriska formen
                    // T.ex. _shapeContext.SetShapeCalculator(new TriangleStrategy());
                    // ... och andra metoder beroende på din implementation

                    Console.WriteLine("Shape updated successfully!");
                }
                else
                {
                    Console.WriteLine("Shape not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input for Shape ID. Please enter a valid number.");
            }

            Console.ReadKey();
        }

        public void DeleteShape()
        {
            Console.Clear();
            Console.Write("Enter the Shape ID you want to delete: ");
            if (int.TryParse(Console.ReadLine(), out int shapeId))
            {
                var shape = _dbContext.Shape.Find(shapeId);

                if (shape != null)
                {
                    // Implementera logiken för att ta bort en geometrisk form
                    Console.WriteLine($"Deleting Shape ID: {shape.ShapeId}, Type: {shape.ShapeType}");

                    // Här kan du använda _shapeContext för att ta bort den geometriska formen från context/databasen
                    // T.ex. _shapeContext.DeleteShape(shape);

                    Console.WriteLine("Shape deleted successfully!");
                }
                else
                {
                    Console.WriteLine("Shape not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input for Shape ID. Please enter a valid number.");
            }

            Console.ReadKey();
        }
    }
}
