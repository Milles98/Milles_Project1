using System.ComponentModel.DataAnnotations;

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
