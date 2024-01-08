using Milles_Project1Library.Interfaces;
using Milles_Project1Library.Interfaces.StrategyInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milles_Project1Library.Services.ShapeStrategyService
{
    public class TriangleStrategy : IShapeStrategy, IShapeDimensionsProvider
    {
        public decimal Base { get; set; }
        public decimal Height { get; set; }
        public decimal SideLength { get; set; }
        public string ShapeType => "Triangle";

        public decimal CalculateArea()
        {
            // Använd exempelvis Herons formel för att beräkna området av en triangel
            decimal s = (Base + Height + SideLength) / 2;
            return (decimal)Math.Sqrt((double)(s * (s - Base) * (s - Height) * (s - SideLength)));
        }

        public decimal CalculatePerimeter()
        {
            return Base + Height + SideLength;
        }

        public void SetDimensions(params decimal[] dimensions)
        {
            if (dimensions.Length == 3)
            {
                Base = dimensions[0];
                Height = dimensions[1];
                SideLength = dimensions[2];
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
