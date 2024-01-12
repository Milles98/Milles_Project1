using Milles_Project1Library.ExtraServices;
using Milles_Project1Library.Interfaces;
using Milles_Project1Library.Interfaces.StrategyInterface;

namespace Milles_Project1Library.Services.ShapeStrategyService
{
    public class Rhombus : IShapeStrategy, IShapeDimensionsProvider
    {
        public decimal Base { get; set; }
        public decimal Height { get; set; }
        public decimal? SideLength { get; set; }
        public string ShapeType => "Rhombus";

        public void SetDimensions(params decimal[] dimensions)
        {
            if (dimensions.Length == 2)
            {
                Base = dimensions[0];
                Height = dimensions[1];

            }
            else
            {
                Message.RedMessage("Incorrect dimensions for a rhombus");
            }
        }


        public int GetDimensionCount()
        {
            return 2;
        }

        public decimal CalculateArea()
        {
            return (Base * Height) / 2;
        }

        public decimal CalculatePerimeter()
        {
            return 4 * (decimal)Math.Sqrt((double)(Base * Base + Height * Height) / 4);
        }
    }

}
