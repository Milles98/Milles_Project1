﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Milles_Project1Library.ExtraServices;

namespace Milles_Project1.Menus
{
    public static class MainMenu
    {
        public static void ShowMenu()
        {
            int choice;

            do
            {
                Console.Clear();
                Console.WriteLine("╭─────────────────────────╮");
                Console.WriteLine("│  Main Menu              │");
                Console.WriteLine("│ 1. Shapes               │");
                Console.WriteLine("│ 2. Calculator           │");
                Console.WriteLine("│ 3. Rock Papeer Scissors │");
                Console.WriteLine("│ 0. Exit Program         │");
                Console.WriteLine("╰─────────────────────────╯");

                Console.Write("Enter your choice: ");
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                        case 5:
                            break;
                        case 0:
                            Console.WriteLine("Exiting program...");
                            Environment.Exit(0);
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