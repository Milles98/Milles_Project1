using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milles_Project1Library.Models
{
    public class GameStatistics
    {
        [Key]
        public int StatisticsId { get; set; }

        public int TotalGamesPlayed { get; set; }
        public int TotalWins { get; set; }
        public int TotalLosses { get; set; }
        public int TotalDraws { get; set; }
        public double AverageWins { get; set; }

    }
}
