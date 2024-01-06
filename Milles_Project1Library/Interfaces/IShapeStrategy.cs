using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milles_Project1Library.Interfaces
{
    public interface IShapeStrategy
    {
        double CalculateArea();
        double CalculatePerimeter();
        void SetDimensions(params double[] dimensions);
        string ShapeType { get; }
    }
}
