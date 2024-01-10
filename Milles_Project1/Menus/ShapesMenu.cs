using Milles_Project1Library.ExtraServices;
using Milles_Project1Library.Interfaces.ServiceInterface;

namespace Milles_Project1.Menus
{
    public static class ShapesMenu
    {
        public static void ShowShapesMenu(IShapeService shapeService)
        {
            int choice;

            do
            {
                Console.Clear();
                Message.ProjectMessage();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("╭────────────────────╮");
                Console.WriteLine("│ Shapes Menu        │");
                Console.WriteLine("│1. Create           │");
                Console.WriteLine("│2. Read             │");
                Console.WriteLine("│3. Update           │");
                Console.WriteLine("│4. Delete           │");
                Console.WriteLine("│0. Return to Menu   │");
                Console.WriteLine("╰────────────────────╯");
                Console.ResetColor();

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
                            Console.WriteLine("Press any key to continue.");
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
