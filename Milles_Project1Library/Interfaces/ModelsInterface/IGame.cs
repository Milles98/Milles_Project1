using Milles_Project1Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milles_Project1Library.Interfaces.ModelsInterface
{
    public interface IGame
    {
        public int GameId { get; set; }

        public Move PlayerMove { get; set; }

        public Move ComputerMove { get; set; }

        public GameResult Result { get; set; }

        public DateTime GameDate { get; set; }

        public bool? IsActive { get; set; }

        public double AverageWins { get; set; }
    }
}
