using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Milles_Project1Library.Models
{
    public class Game
    {
        [Key]
        public int GameId { get; set; }

        public string PlayerMove { get; set; }

        public string ComputerMove { get; set; }

        public string Result { get; set; }

        public DateTime GameDate { get; set; }

        public double AverageWins { get; set; }
    }
}
