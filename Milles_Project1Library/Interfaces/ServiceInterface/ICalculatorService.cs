using Milles_Project1Library.Models;

namespace Milles_Project1Library.Interfaces.ServiceInterface
{
    public interface ICalculatorService
    {
        public void PerformCreateCalculation();
        public void ReadCalculation();

        public void UpdateCalculation();

        public void DeleteCalculation();

        public void UpdateCalculationInDatabase(Calculator calculation, decimal newNum1, decimal newNum2);

        public void DeleteCalculationFromDatabase(Calculator calculation);
    }
}
