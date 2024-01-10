using Autofac;
using Milles_Project1.Menus;
using Milles_Project1Library.Data;
using Milles_Project1Library.Interfaces.ServiceInterface;

namespace Milles_Project1
{
    public class App
    {
        private readonly IContainer _container;

        public App(IContainer container)
        {
            _container = container;
        }

        public void RunApplication()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var calculatorService = scope.Resolve<ICalculatorService>();
                var shapeService = scope.Resolve<IShapeService>();
                var gameService = scope.Resolve<IGameService>();
                var dataSeeding = scope.Resolve<DataSeeding>();

                dataSeeding.Seed();

                while (true)
                {
                    MainMenu.ShowMenu(calculatorService, shapeService, gameService);
                }
            }
        }
    }
}
