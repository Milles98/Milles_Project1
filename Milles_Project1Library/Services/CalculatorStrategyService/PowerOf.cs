using Milles_Project1Library.ExtraServices;
using Milles_Project1Library.Interfaces.StrategyInterface;

namespace Milles_Project1Library.Services.CalculatorStrategyService
{
    public class PowerOf : ICalculatorStrategy
    {
        public decimal Calculate(decimal num1, decimal num2)
        {
            if (IsResultOutOfRange(num1, num2))
            {
                Message.ErrorMessage("Result is too large. Please try again with smaller numbers.");
                return 0;
            }

            return (decimal)Math.Pow((double)num1, (double)num2);
        }

        private bool IsResultOutOfRange(decimal num1, decimal num2)
        {
            const double MaxAllowedResult = 1e308;

            double result = Math.Pow((double)num1, (double)num2);

            return Math.Abs(result) > MaxAllowedResult;
        }
    }
}
