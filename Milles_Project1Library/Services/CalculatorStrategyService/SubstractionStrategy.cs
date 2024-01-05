using Milles_Project1Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milles_Project1Library.Services.CalculatorStrategyService
{
    public class SubtractionStrategy : ICalculatorStrategy
    {
        public double Calculate(double num1, double num2)
        {
            return num1 - num2;
        }
    }
}
