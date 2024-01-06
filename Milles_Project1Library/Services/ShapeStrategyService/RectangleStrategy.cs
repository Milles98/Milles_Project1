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
        private decimal _width;
        private decimal _height;
        public string ShapeType => "Rectangle";

        public decimal CalculateArea()
        {
            return _width * _height;
        }

        public decimal CalculatePerimeter()
        {
            return 2 * (_width + _height);
        }

        public void SetDimensions(params decimal[] dimensions)
        {
            if (dimensions.Length == 2)
            {
                _width = dimensions[0];
                _height = dimensions[1];
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
