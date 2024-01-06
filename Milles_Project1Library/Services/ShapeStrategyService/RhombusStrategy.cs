using Milles_Project1Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milles_Project1Library.Services.ShapeStrategyService
{
    public class RhombusStrategy : IShapeStrategy, IShapeDimensionsProvider
    {
        public double Diagonal1 { get; set; }
        public double Diagonal2 { get; set; }
        public string ShapeType => "Rhombus";

        public double CalculateArea()
        {
            return (Diagonal1 * Diagonal2) / 2;
        }

        public double CalculatePerimeter()
        {
            // Assuming all sides of the rhombus are equal
            return 4 * Math.Sqrt((Diagonal1 * Diagonal1 + Diagonal2 * Diagonal2) / 4);
        }

        public void SetDimensions(params double[] dimensions)
        {
            if (dimensions.Length == 2)
            {
                Diagonal1 = dimensions[0];
                Diagonal2 = dimensions[1];
            }
            else
            {
                // Felhantering om antalet dimensioner inte är korrekt för en romb
            }
        }

        public int GetDimensionCount()
        {
            return 2;
        }
    }
}
