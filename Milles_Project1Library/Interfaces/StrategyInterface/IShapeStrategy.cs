namespace Milles_Project1Library.Interfaces.StrategyInterface
{
    public interface IShapeStrategy : IShapeDimensionsProvider
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
