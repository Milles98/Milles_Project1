using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milles_Project1Library.Models
{
    public class Game
    {
        public int GameId { get; set; }

        public string PlayerMove { get; set; }

        public string ComputerMove { get; set; }

        public string Result { get; set; }

        public DateTime GameDate { get; set; }

        public double AverageWins { get; set; }
    }
}
