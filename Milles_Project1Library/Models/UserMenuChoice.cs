using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milles_Project1Library.Models
{
    public class UserMenuChoice
    {
        public int UserMenuChoiceId { get; set; }
        public string MenuType { get; set; } // Exempel: Shapes, Calculator, Game
        public string Choice { get; set; } // Exempel: C, R, U, D, R (för att se tidigare spel)
        public DateTime DateSelected { get; set; }
    }
}
