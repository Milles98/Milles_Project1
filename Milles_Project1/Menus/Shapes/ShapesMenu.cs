using Milles_Project1Library.ExtraServices;
using Milles_Project1Library.Interfaces;
using Milles_Project1Library.Services.ShapeStrategyService;
using Milles_Project1Library.StrategyContext;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Milles_Project1Library.Services;
using Milles_Project1Library.Data;

namespace Milles_Project1.Menus.Shapes
{
    public static class ShapesMenu
    {
        public static void ShowShapesMenu(IShapeContext shapeContext)
        {

            int choice;

            do
            {
                Console.Clear();
                Console.WriteLine("╭────────────────────────────╮");
                Console.WriteLine("│Shapes Menu                 │");
                Console.WriteLine("│1. Calculate Rectangle      │");
                Console.WriteLine("│2. Calculate Parallellogram │");
                Console.WriteLine("│3. Calculate Triangle       │");
                Console.WriteLine("│4. Calculate Rhombus        │");
                Console.WriteLine("│0. Return to MainMenu       │");
                Console.WriteLine("╰────────────────────────────╯");

                Console.Write("Enter your choice: ");
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            shapeContext.SetShapeCalculator(new RectangleStrategy());
                            shapeContext.CalculateAndDisplayResults();
                            break;
                        case 2:
                            shapeContext.SetShapeCalculator(new ParallelogramStrategy());
                            shapeContext.CalculateAndDisplayResults();
                            break;
                        case 3:
                            shapeContext.SetShapeCalculator(new TriangleStrategy());
                            shapeContext.CalculateAndDisplayResults();
                            break;
                        case 4:
                            shapeContext.SetShapeCalculator(new RhombusStrategy());
                            shapeContext.CalculateAndDisplayResults();
                            break;
                        case 0:
                            Console.WriteLine("Returning to MainMenu...");
                            break;
                        default:
                            Message.ErrorMessage("Invalid choice. Please try again.");
                            Thread.Sleep(1000);
                            break;
                    }
                }
                else
                {
                    Message.ErrorMessage("Invalid input. Please enter a number.");
                    Thread.Sleep(1000);
                }

            } while (choice != 0);
        }
    }
}
