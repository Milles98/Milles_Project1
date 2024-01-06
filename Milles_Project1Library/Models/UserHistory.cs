using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Milles_Project1Library.Models
{
    public class UserHistory
    {
        [Key]
        public int UserHistoryId { get; set; }
        public string ActionType { get; set; } // Exempel: Shapes, Calculator, Game
        public string Action { get; set; } // Exempel: C, R, U, D, R (för att se tidigare spel)
        public DateTime DatePerformed { get; set; }
    }
}
