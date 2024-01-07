using Microsoft.EntityFrameworkCore;
using Milles_Project1Library.Models;
using Milles_Project1Library.StrategyContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milles_Project1Library.Interfaces
{
    public interface ICalculatorService
    {
        public void ReadCalculation();

        public void UpdateCalculation();

        public void DeleteCalculation();

        public void UpdateCalculationInDatabase(Calculator calculation, decimal newNum1, decimal newNum2);

        public void DeleteCalculationFromDatabase(Calculator calculation);
    }
}
