using Milles_Project1Library.Interfaces.FactoryInterface;
using Milles_Project1Library.Interfaces.ServiceInterface;
using Milles_Project1Library.Menus;

namespace Milles_Project1Library.FactoryMenus
{
    public class CalculatorMenuFactory : IMenuFactory
    {
        private readonly ICalculatorService _calculatorService;

        public CalculatorMenuFactory(ICalculatorService calculatorService)
        {
            _calculatorService = calculatorService;
        }

        public IMenu CreateMenu()
        {
            return new CalculatorMenu(_calculatorService);
        }
    }
}
