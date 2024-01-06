using Milles_Project1Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milles_Project1Library.StrategyContext
{
    public class CalculatorContext : ICalculatorContext
    {
        private ICalculatorStrategy _strategy;

        public CalculatorContext(ICalculatorStrategy strategy)
        {
            _strategy = strategy;
        }

        public double ExecuteOperation(double num1, double num2)
        {
            return _strategy.Calculate(num1, num2);
        }

        public void SetStrategy(ICalculatorStrategy strategy)
        {
            _strategy = strategy;
        }
    }
}
