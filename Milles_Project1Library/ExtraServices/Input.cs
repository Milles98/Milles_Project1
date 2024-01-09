using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milles_Project1Library.ExtraServices
{
    public static class Input
    {
        public static int NumberInput(int maximumInput)
        {
            var choice = 0;
            Console.Write($"Enter a number between 1 and {maximumInput}, or press 'e' to exit: ");
            while (true)
            {
                string? input = Console.ReadLine();
                if (input?.ToLower() == "e")
                {
                    return -1;
                }
                if (int.TryParse(input, out choice))
                {
                    if (choice >= 1 && choice <= maximumInput)
                    {
                        return choice;
                    }
                    else
                    {
                        Console.WriteLine($"Please enter a number between 1 and {maximumInput}.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number or 'e' to exit.");
                }
            }
        }
    }
}
