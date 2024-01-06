﻿using Milles_Project1Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milles_Project1Library.Services.CalculatorStrategyService
{
    public class DivisionStrategy : ICalculatorStrategy
    {
        public decimal Calculate(decimal num1, decimal num2)
        {
            return num1 / num2;
        }
    }
}
