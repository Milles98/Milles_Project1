using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Milles_Project1Library.Models
{
    public class Calculator
    {
        [Key]
        public int CalculationId { get; set; }

        public double Number1 { get; set; }
        public double Number2 { get; set; }

        public string Operator { get; set; }

        public double Result { get; set; }

        public DateTime CalculationDate { get; set; }
    }
}
