using Milles_Project1Library.Interfaces.StrategyInterface;

namespace Milles_Project1Library.Services.CalculatorStrategyService
{
    public class Multiplication : ICalculatorStrategy
    {
        public decimal Calculate(decimal num1, decimal num2)
        {
            return num1 * num2;
        }
    }
}
