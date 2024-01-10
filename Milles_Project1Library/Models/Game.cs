using System.ComponentModel.DataAnnotations;
using Milles_Project1Library.Interfaces.ModelsInterface;

namespace Milles_Project1Library.Models
{
    public enum Move
    {
        Rock,
        Paper,
        Scissor
    }

    public enum GameResult
    {
        Win,
        Loss,
        Draw
    }
    public class Game : IGame
    {
        [Key]
        public int GameId { get; set; }

        public Move PlayerMove { get; set; }

        public Move ComputerMove { get; set; }

        public GameResult Result { get; set; }

        public DateTime GameDate { get; set; }

        public bool? IsActive { get; set; }

        public double AverageWins { get; set; }

        public virtual List<GameHistory> GameHistories { get; set; }

        public int? GameStatisticsId { get; set; }
        public virtual GameStatistics GameStatistics { get; set; }
    }
}
