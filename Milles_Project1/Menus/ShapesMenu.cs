using Milles_Project1Library.ExtraServices;
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
using Milles_Project1Library.Interfaces.ServiceInterface;
using Milles_Project1Library.Interfaces.ContextInterface;

namespace Milles_Project1.Menus
{
    public static class ShapesMenu
    {
        public static void ShowShapesMenu(IShapeContext shapeContext, IShapeService shapeService)
        {
            int choice;

            do
            {
                Console.Clear();
                Console.WriteLine("╭────────────────────╮");
                Console.WriteLine("│ Shapes Menu        │");
                Console.WriteLine("│1. Create Shape     │");
                Console.WriteLine("│2. Read Shapes      │");
                Console.WriteLine("│3. Update Shape     │");
                Console.WriteLine("│4. Delete Shape     │");
                Console.WriteLine("│0. Return to Menu   │");
                Console.WriteLine("╰────────────────────╯");

                Console.Write("Enter your choice: ");
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            shapeService.CreateShape();
                            break;
                        case 2:
                            shapeService.ReadShapes();
                            Console.ReadKey();
                            break;
                        case 3:
                            shapeService.UpdateShape();
                            break;
                        case 4:
                            shapeService.DeleteShape();
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
