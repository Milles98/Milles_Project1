using Milles_Project1Library.Interfaces;
using Milles_Project1Library.Interfaces.StrategyInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milles_Project1Library.Services.ShapeStrategyService
{
    public class RhombusStrategy : IShapeStrategy, IShapeDimensionsProvider
    {
        public decimal Base { get; set; }
        public decimal Height { get; set; }
        public decimal SideLength { get; set; }
        public string ShapeType => "Rhombus";

        public decimal CalculateArea()
        {
            return (Base * Height) / 2;
        }

        public decimal CalculatePerimeter()
        {
            // Assuming all sides of the rhombus are equal
            return 4 * (decimal)Math.Sqrt((double)(Base * Base + Height * Height) / 4);
        }

        public void SetDimensions(params decimal[] dimensions)
        {
            if (dimensions.Length == 2)
            {
                Base = dimensions[0];
                Height = dimensions[1];
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
