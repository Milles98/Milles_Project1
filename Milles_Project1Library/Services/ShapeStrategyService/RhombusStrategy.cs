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
        public decimal Diagonal1 { get; set; }
        public decimal Diagonal2 { get; set; }
        public string ShapeType => "Rhombus";

        public decimal CalculateArea()
        {
            return (Diagonal1 * Diagonal2) / 2;
        }

        public decimal CalculatePerimeter()
        {
            // Assuming all sides of the rhombus are equal
            return 4 * (decimal)Math.Sqrt((double)(Diagonal1 * Diagonal1 + Diagonal2 * Diagonal2) / 4);
        }

        public void SetDimensions(params decimal[] dimensions)
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
