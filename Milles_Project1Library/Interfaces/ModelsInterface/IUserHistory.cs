using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milles_Project1Library.Interfaces.ModelsInterface
{
    public interface IUserHistory
    {
        public int UserHistoryId { get; set; }
        public string ActionType { get; set; } // Exempel: Shapes, Calculator, Game
        public string Action { get; set; } // Exempel: C, R, U, D, R (för att se tidigare spel)
        public DateTime DatePerformed { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
