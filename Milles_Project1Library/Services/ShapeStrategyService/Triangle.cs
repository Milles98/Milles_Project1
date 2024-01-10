using Milles_Project1Library.ExtraServices;
using Milles_Project1Library.Interfaces;
using Milles_Project1Library.Interfaces.StrategyInterface;

namespace Milles_Project1Library.Services.ShapeStrategyService
{
    public class Triangle : IShapeStrategy, IShapeDimensionsProvider
    {
        public decimal Base { get; set; }
        public decimal Height { get; set; }
        public decimal SideLength { get; set; }
        public string ShapeType => "Triangle";

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
                Message.ErrorMessage("Incorrect dimensions for a triangle");
            }
        }

        public int GetDimensionCount()
        {
            return 3;
        }

        public decimal CalculateArea()
        {
            decimal s = (Base + Height + SideLength) / 2;
            double area = Math.Sqrt((double)(s * (s - Base) * (s - Height) * (s - SideLength)));

            if (area >= (double)decimal.MinValue && area <= (double)decimal.MaxValue)
            {
                return new decimal(area);
            }
            else
            {
                throw new OverflowException("Calculated area is outside the valid range for a decimal.");
            }
        }

        public decimal CalculatePerimeter()
        {
            return Base + Height + SideLength;
        }
    }

}
