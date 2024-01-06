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
        private double _width;
        private double _height;
        public string ShapeType => "Rectangle";

        public double CalculateArea()
        {
            return _width * _height;
        }

        public double CalculatePerimeter()
        {
            return 2 * (_width + _height);
        }

        public void SetDimensions(params double[] dimensions)
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
