using Microsoft.EntityFrameworkCore;
using Milles_Project1Library.ExtraServices;
using Milles_Project1Library.Interfaces.StrategyInterface;
using Milles_Project1Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milles_Project1Library.Interfaces.ContextInterface
{
    public interface ICalculatorContext
    {
        decimal ExecuteOperation(decimal num1, decimal num2);
        decimal GetUserInput(string prompt);
        void CalculateAndDisplayResults();
        void SetStrategy(ICalculatorStrategy strategy);
        public void CreateCalculation(decimal num1, decimal num2);
        public IEnumerable<Calculator> ReadCalculation();
        public void UpdateCalculation(int calculationId, decimal num1, decimal num2);
        public void DeleteCalculation(int calculationId);
        public void SaveCalculationToDatabase(decimal num1, decimal num2, decimal result);
    }
}
