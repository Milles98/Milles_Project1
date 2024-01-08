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
    public class UserHistoryService : IUserHistoryService
    {
        private readonly ProjectDbContext _dbContext;

        public UserHistoryService(ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void ShowUserHistory()
        {
            Console.Clear();
            Console.WriteLine("╭────────────────────╮");
            Console.WriteLine("│ User History       │");
            Console.WriteLine("╰────────────────────╯");

            List<UserHistory> userHistoryEntries = GetUserHistoryEntries();

            if (userHistoryEntries.Any())
            {
                foreach (var entry in userHistoryEntries)
                {
                    Console.WriteLine($"ID: {entry.UserHistoryId}");
                    Console.WriteLine($"Action Type: {entry.ActionType}");
                    Console.WriteLine($"Action: {entry.Action}");
                    Console.WriteLine($"Date Performed: {entry.DatePerformed}");
                    Console.WriteLine($"Description: {entry.Description}");
                    Console.WriteLine("-------------------------------------");
                }
            }
            else
            {
                Console.WriteLine("No user history entries found.");
            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        private List<UserHistory> GetUserHistoryEntries()
        {
            return _dbContext.UserHistory
                .OrderByDescending(entry => entry.DatePerformed)
                .ToList();
        }
    }
}
