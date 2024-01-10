using Milles_Project1Library.ExtraServices;
using Milles_Project1Library.Interfaces;
using Milles_Project1Library.Interfaces.StrategyInterface;

namespace Milles_Project1Library.Services.ShapeStrategyService
{
    public class Rectangle : IShapeStrategy, IShapeDimensionsProvider
    {
        public decimal Base { get; set; }
        public decimal Height { get; set; }
        public decimal SideLength { get; set; }
        public string ShapeType => "Rectangle";

        public void SetDimensions(params decimal[] dimensions)
        {
            if (dimensions.Length == 2)
            {
                Height = dimensions[0];
                Base = dimensions[1];

            }
            else
            {
                Message.RedMessage("Incorrect dimensions for a rectangle");
            }
        }

        public int GetDimensionCount()
        {
            return 2;
        }

        public decimal CalculateArea()
        {
            return Height * Base;
        }

        public decimal CalculatePerimeter()
        {
            return 2 * (Height + Base);
        }
    }

}
