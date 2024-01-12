using Milles_Project1Library.ExtraServices;
using Milles_Project1Library.Interfaces.StrategyInterface;

namespace Milles_Project1Library.Services.CalculatorStrategyService
{
    public class SquareRoot : ICalculatorStrategy
    {
        public decimal Calculate(decimal num1, decimal? num2)
        {
            return (decimal)Math.Sqrt((double)num1);
        }

    }
}
