using Milles_Project1Library.Interfaces.StrategyInterface;

namespace Milles_Project1Library.Interfaces.ContextInterface
{
    public interface IShapeContext
    {
        void SetShapeCalculator(IShapeStrategy shapeStrategy);
        public void SetShapeProperties(decimal[] dimensions);
        public decimal[] GetDimensionsInput();
        void CalculateAndDisplayResults();
    }
}
