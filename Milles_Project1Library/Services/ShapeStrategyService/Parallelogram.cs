using Milles_Project1Library.ExtraServices;
using Milles_Project1Library.Interfaces;
using Milles_Project1Library.Interfaces.StrategyInterface;

namespace Milles_Project1Library.Services.ShapeStrategyService
{
    public class Parallelogram : IShapeStrategy, IShapeDimensionsProvider
    {
        public decimal Base { get; set; }
        public decimal Height { get; set; }
        public decimal? SideLength { get; set; }
        public string ShapeType => "Parallelogram";

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
                Message.RedMessage("Incorrect dimensions for a parallelogram");
            }
        }

        public int GetDimensionCount()
        {
            return 3;
        }

        public decimal CalculateArea()
        {
            return Base * Height;
        }

        public decimal CalculatePerimeter()
        {
            return 2 * (Base + SideLength.Value);
        }
    }

}
