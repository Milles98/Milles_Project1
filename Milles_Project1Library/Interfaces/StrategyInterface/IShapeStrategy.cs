﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milles_Project1Library.Interfaces.StrategyInterface
{
    public interface IShapeStrategy
    {
        decimal CalculateArea();
        decimal CalculatePerimeter();
        void SetDimensions(params decimal[] dimensions);
        string ShapeType { get; }
        decimal Base { get; }
        decimal Height { get; }
        decimal SideLength { get; }
    }
}