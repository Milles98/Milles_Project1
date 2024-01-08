using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Milles_Project1Library.Interfaces.StrategyInterface;

namespace Milles_Project1Library.Interfaces.ContextInterface
{
    public interface IShapeContext
    {
        void SetShapeCalculator(IShapeStrategy shapeStrategy);

        void CalculateAndDisplayResults();
    }
}
