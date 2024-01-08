using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milles_Project1Library.Models
{
    public class GameHistory
    {
        [Key]
        public int GameHistoryId { get; set; }

        public int GameId { get; set; }
        public virtual Game Game { get; set; }

        public string Winner { get; set; } // "Player", "Computer", or "Draw"
        public int RoundsTaken { get; set; }
        public Move WinningMove { get; set; }
        public DateTime GameEndDate { get; set; }
    }
}
