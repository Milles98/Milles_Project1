﻿using Milles_Project1Library.ExtraServices;
using Milles_Project1Library.Interfaces;
using Milles_Project1Library.Interfaces.StrategyInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milles_Project1Library.Services.ShapeStrategyService
{
    public class RectangleStrategy : IShapeStrategy, IShapeDimensionsProvider
    {
        private const int MaxDigits = 3;

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

                ValidateDimensions();
            }
            else
            {
                Message.ErrorMessage("Incorrect dimensions for a rectangle");
            }
        }

        private void ValidateDimensions()
        {
            ValidateDigitCount(Height, "Height");
            ValidateDigitCount(Base, "Base");
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
