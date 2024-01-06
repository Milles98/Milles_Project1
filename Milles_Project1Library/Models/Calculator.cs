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

        public decimal Number1 { get; set; }
        public decimal Number2 { get; set; }

        public string Operator { get; set; }

        public decimal Result { get; set; }

        public DateTime CalculationDate { get; set; }
    }
}
