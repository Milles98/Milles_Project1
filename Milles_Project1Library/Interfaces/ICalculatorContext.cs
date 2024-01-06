using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milles_Project1Library.Interfaces
{
    public interface ICalculatorContext
    {
        double ExecuteOperation(double num1, double num2);

        void SetStrategy(ICalculatorStrategy strategy);
    }
}
