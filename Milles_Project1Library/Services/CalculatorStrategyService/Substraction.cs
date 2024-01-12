using Milles_Project1Library.Interfaces.StrategyInterface;

namespace Milles_Project1Library.Services.CalculatorStrategyService
{
    public class Subtraction : ICalculatorStrategy
    {
        public decimal Calculate(decimal num1, decimal? num2)
        {
            return num2.HasValue ? num1 - num2.Value : num1;
        }
    }
}
