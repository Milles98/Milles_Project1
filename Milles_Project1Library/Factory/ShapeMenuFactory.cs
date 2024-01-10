using Autofac;
using Milles_Project1Library.Interfaces.FactoryInterface;
using Milles_Project1Library.Interfaces.ServiceInterface;
using Milles_Project1Library.Menus;

namespace Milles_Project1Library.Factory
{
    public class ShapeMenuFactory : IMenuFactory
    {
        private readonly IShapeService _shapeService;

        public ShapeMenuFactory(IShapeService shapeService)
        {
            _shapeService = shapeService;
        }

        public IMenu CreateMenu()
        {
            return new ShapesMenu(_shapeService);
        }
    }
}
