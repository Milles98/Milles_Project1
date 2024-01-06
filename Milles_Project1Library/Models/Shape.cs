using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milles_Project1Library.Models
{
    public class Shape
    {
        public int ShapeId { get; set; }

        public string ShapeType { get; set; }

        public double Area { get; set; }

        public double Perimeter { get; set; }

        public DateTime CalculationDate { get; set; }
    }
}
