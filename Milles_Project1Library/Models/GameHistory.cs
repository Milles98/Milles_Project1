using Milles_Project1Library.Interfaces.ModelsInterface;
using System.ComponentModel.DataAnnotations;

namespace Milles_Project1Library.Models
{
    public class GameHistory : IGameHistory
    {
        [Key]
        public int GameHistoryId { get; set; }
        public int GameId { get; set; }
        public virtual Game Game { get; set; }
        public string Winner { get; set; }
        public int RoundsTaken { get; set; }
        public Move WinningMove { get; set; }
        public DateTime GameEndDate { get; set; }
    }
}
