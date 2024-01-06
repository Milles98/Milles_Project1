using Milles_Project1Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milles_Project1Library.Services.ShapeStrategyService
{
    public class TriangleStrategy : IShapeStrategy, IShapeDimensionsProvider
    {
        private decimal[] sides = new decimal[3];
        public string ShapeType => "Triangle";

        public decimal CalculateArea()
        {
            // Heron's formula for area of a triangle
            var s = CalculatePerimeter() / 2;
            return (decimal)Math.Sqrt((double)(s * (s - sides[0]) * (s - sides[1]) * (s - sides[2])));
        }

        public decimal CalculatePerimeter()
        {
            return sides.Sum();
        }

        public void SetDimensions(params decimal[] dimensions)
        {
            if (dimensions.Length == 3)
            {
                for (int i = 0; i < 3; i++)
                {
                    sides[i] = dimensions[i];
                }
            }
            else
            {
                // Felhantering om antalet dimensioner inte är korrekt för triangel
            }
        }

        public int GetDimensionCount()
        {
            return 3;
        }
    }
}
