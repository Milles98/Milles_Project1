using Milles_Project1Library.Data;
using Milles_Project1Library.ExtraServices;
using Milles_Project1Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milles_Project1.Menus
{
    public class UserHistoryMenu
    {
        public static void ShowUserHistoryMenu(ProjectDbContext dbContext, IUserHistoryService userHistoryService)
        {
            int choice;

            do
            {
                Console.Clear();
                Console.WriteLine("╭────────────────────╮");
                Console.WriteLine("│ User History Menu  │");
                Console.WriteLine("│1. Show History     │");
                Console.WriteLine("│0. Return to Menu   │");
                Console.WriteLine("╰────────────────────╯");

                Console.Write("Enter your choice: ");
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            userHistoryService.ShowUserHistory();
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
