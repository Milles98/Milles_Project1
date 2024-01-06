using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milles_Project1Library.Interfaces
{
    public interface ICalculatorContext
    {
        decimal ExecuteOperation(decimal num1, decimal num2);
        void CalculateAndDisplayResults();

        void SetStrategy(ICalculatorStrategy strategy);
    }
}
