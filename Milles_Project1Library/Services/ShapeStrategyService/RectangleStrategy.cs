using Milles_Project1Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milles_Project1Library.Services.ShapeStrategyService
{
    public class RectangleStrategy : IShapeStrategy, IShapeDimensionsProvider
    {
        public decimal Base { get; set; }
        public decimal Height { get; set; }
        public decimal SideLength { get; set; }
        public string ShapeType => "Rectangle";

        public decimal CalculateArea()
        {
            return Height * Base;
        }

        public decimal CalculatePerimeter()
        {
            return 2 * (Height + Base);
        }

        public void SetDimensions(params decimal[] dimensions)
        {
            if (dimensions.Length == 2)
            {
                Height = dimensions[0];
                Base = dimensions[1];
            }
            else
            {
                // Felhantering om antalet dimensioner inte är korrekt för rektangel
            }
        }

        public int GetDimensionCount()
        {
            return 2;
        }
    }
}
