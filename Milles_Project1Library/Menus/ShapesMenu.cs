using Milles_Project1Library.ExtraServices;
using Milles_Project1Library.Interfaces.FactoryInterface;
using Milles_Project1Library.Interfaces.ServiceInterface;

namespace Milles_Project1Library.Menus
{
    public class ShapesMenu : IMenu
    {
        private readonly IShapeService _shapeService;

        public ShapesMenu(IShapeService shapeService)
        {
            _shapeService = shapeService;
        }

        public void ShowMenu()
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
                Console.WriteLine("│5. Re Activate      │");
                Console.WriteLine("│0. Return to Menu   │");
                Console.WriteLine("╰────────────────────╯");
                Console.ResetColor();

                Console.Write("Enter your choice: ");
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            _shapeService.CreateShape();
                            break;
                        case 2:
                            _shapeService.ReadShapes();
                            Console.WriteLine("Press any key to continue.");
                            Console.ReadKey();
                            break;
                        case 3:
                            _shapeService.UpdateShape();
                            break;
                        case 4:
                            _shapeService.DeleteShape();
                            break;
                        case 5:
                            _shapeService.ReActivateShape();
                            break;
                        case 0:
                            Console.WriteLine("Returning to MainMenu...");
                            break;
                        default:
                            Message.RedMessage("Invalid choice. Please try again.");
                            Thread.Sleep(1000);
                            break;
                    }
                }
                else
                {
                    Message.RedMessage("Invalid input. Please enter a number.");
                    Thread.Sleep(1000);
                }

            } while (choice != 0);
        }
        public Type GetMenuType()
        {
            return this.GetType();
        }
    }
}
