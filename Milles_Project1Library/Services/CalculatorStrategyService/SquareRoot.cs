using Milles_Project1Library.ExtraServices;
using Milles_Project1Library.Interfaces.StrategyInterface;

namespace Milles_Project1Library.Services.CalculatorStrategyService
{
    public class SquareRoot : ICalculatorStrategy
    {
        public decimal Calculate(decimal num1, decimal num2)
        {
            if (IsResultOutOfRange(num1, num2))
            {
                Message.RedMessage("Result is too large. Please try again with smaller numbers.");
                return 0;
            }

            return (decimal)Math.Sqrt((double)num1);
        }

        private bool IsResultOutOfRange(decimal num1, decimal num2)
        {
            const double MaxAllowedResult = 1e308;

            double squareRoot = Math.Sqrt((double)num1);
            if (double.IsInfinity(squareRoot) || double.IsNaN(squareRoot))
            {
                return true;
            }

            double result = Math.Pow(squareRoot, (double)num2);

            return Math.Abs(result) > MaxAllowedResult;
        }
    }
}
