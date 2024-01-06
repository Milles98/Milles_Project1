using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Milles_Project1Library.Models
{
    public class Shape
    {
        [Key]
        public int ShapeId { get; set; }

        public string ShapeType { get; set; }

        public decimal Area { get; set; }

        public decimal Perimeter { get; set; }

        public DateTime CalculationDate { get; set; }
    }
}
