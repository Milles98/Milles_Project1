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
        private double[] sides = new double[3];

        public double CalculateArea()
        {
            // Heron's formula for area of a triangle
            var s = CalculatePerimeter() / 2;
            return Math.Sqrt(s * (s - sides[0]) * (s - sides[1]) * (s - sides[2]));
        }

        public double CalculatePerimeter()
        {
            return sides.Sum();
        }

        public void SetDimensions(params double[] dimensions)
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
