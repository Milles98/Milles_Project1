using Milles_Project1Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milles_Project1Library.Services.ShapeStrategyService
{
    public class ParallelogramStrategy : IShapeStrategy, IShapeDimensionsProvider
    {
        public double Base { get; set; }
        public double Height { get; set; }
        public double SideLength { get; set; }

        public void SetDimensions(params double[] dimensions)
        {
            if (dimensions.Length == 3)
            {
                Base = dimensions[0];
                Height = dimensions[1];
                SideLength = dimensions[2];
            }
            else
            {
                // Felhantering om antalet dimensioner inte är korrekt för en parallelogram
            }
        }

        public int GetDimensionCount()
        {
            return 3;
        }

        public double CalculateArea()
        {
            return Base * Height;
        }

        public double CalculatePerimeter()
        {
            return 2 * (Base + SideLength);
        }
    }
}
