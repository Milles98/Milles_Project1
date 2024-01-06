using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milles_Project1Library.Interfaces
{
    public interface ICalculatorStrategy
    {
        decimal Calculate(decimal num1, decimal num2);
    }
}
