using Milles_Project1Library.ExtraServices;
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
        private const int MaxDigits = 3;
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

                ValidateDimensions();
            }
            else
            {
                Message.ErrorMessage("Incorrect dimensions for a triangle");
            }
        }

        private void ValidateDimensions()
        {
            ValidateDigitCount(Base, "Base");
            ValidateDigitCount(Height, "Height");
            ValidateDigitCount(SideLength, "Side Length");
        }

        private void ValidateDigitCount(decimal value, string dimensionName)
        {
            string valueAsString = Math.Abs(value).ToString();
            int decimalSeparatorIndex = valueAsString.IndexOf('.');

            int digitsBeforeDecimal = decimalSeparatorIndex == -1 ? valueAsString.Length : decimalSeparatorIndex;
            int digitsAfterDecimal = decimalSeparatorIndex == -1 ? 0 : valueAsString.Length - (decimalSeparatorIndex + 1);

            int totalDigits = digitsBeforeDecimal + digitsAfterDecimal;

            if (totalDigits > MaxDigits)
            {
                Message.ErrorMessage($"{dimensionName} exceeds the maximum allowed digits ({MaxDigits}).");
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
