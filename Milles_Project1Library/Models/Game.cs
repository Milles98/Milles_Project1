using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Milles_Project1Library.Models
{
    public enum Move
    {
        Sten,
        Sax,
        Påse
    }

    public enum GameResult
    {
        Vinst,
        Förlust,
        Oavgjort
    }
    public class Game
    {
        [Key]
        public int GameId { get; set; }

        public Move PlayerMove { get; set; }

        public Move ComputerMove { get; set; }

        public GameResult Result { get; set; }

        public DateTime GameDate { get; set; }

        public bool? IsActive { get; set; }

        public double AverageWins { get; set; }
    }
}
