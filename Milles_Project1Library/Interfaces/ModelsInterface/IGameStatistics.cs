namespace Milles_Project1Library.Interfaces.ModelsInterface
{
    public interface IGameStatistics
    {
        public int StatisticsId { get; set; }
        public int TotalGamesPlayed { get; set; }
        public int TotalWins { get; set; }
        public int TotalLosses { get; set; }
        public int TotalDraws { get; set; }
        public double AverageWins { get; set; }
    }
}
