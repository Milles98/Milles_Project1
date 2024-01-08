using Milles_Project1Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milles_Project1Library.Interfaces.ModelsInterface
{
    public interface IGameHistory
    {
        public int GameHistoryId { get; set; }
        public int GameId { get; set; }
        public string Winner { get; set; }
        public int RoundsTaken { get; set; }
        public Move WinningMove { get; set; }
        public DateTime GameEndDate { get; set; }
    }
}
