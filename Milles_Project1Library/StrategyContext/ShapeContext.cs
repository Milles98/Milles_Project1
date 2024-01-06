using Milles_Project1Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milles_Project1Library.StrategyContext
{
    public class ShapeContext : IShapeContext
    {
        private IShapeStrategy _shapeStrategy;

        public void SetShapeCalculator(IShapeStrategy shapeStrategy)
        {
            _shapeStrategy = shapeStrategy;
        }

        public void CalculateAndDisplayResults()
        {
            if (_shapeStrategy == null)
            {
                Console.WriteLine("No shape calculator selected.");
                return;
            }

            // Assume you have settings or input for the shape, e.g., side lengths, etc.
            double[] dimensions = GetDimensionsInput();

            // Set values on _shapeStrategy based on user input
            SetShapeProperties(dimensions);

            // Calculate and display the results
            double area = _shapeStrategy.CalculateArea();
            double perimeter = _shapeStrategy.CalculatePerimeter();

            Console.WriteLine($"Area: {area}");
            Console.WriteLine($"Perimeter: {perimeter}");

            Console.ReadKey();
        }

        private void SetShapeProperties(double[] dimensions)
        {
            // Assume you set user input values on _shapeStrategy
            // This can vary depending on the type of shape you're working with
            // For example, you might have methods like SetSideLength, SetHeight, etc. in your IShapeStrategy interface
            _shapeStrategy.SetDimensions(dimensions);
        }

        private double[] GetDimensionsInput()
        {
            if (_shapeStrategy is IShapeDimensionsProvider dimensionsProvider)
            {
                int dimensionCount = dimensionsProvider.GetDimensionCount();
                double[] dimensions = new double[dimensionCount];

                for (int i = 0; i < dimensionCount; i++)
                {
                    dimensions[i] = GetDoubleInput($"Enter dimension {i + 1}:");
                }

                return dimensions;
            }

            return Array.Empty<double>();
        }

        private double GetDoubleInput(string prompt)
        {
            double result;
            bool validInput;

            do
            {
                Console.Write(prompt);
                validInput = double.TryParse(Console.ReadLine(), out result);

                if (!validInput)
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }

            } while (!validInput);

            return result;
        }
    }
}
