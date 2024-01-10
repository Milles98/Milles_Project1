using Autofac;
using Milles_Project1Library.Interfaces.FactoryInterface;
using Milles_Project1Library.Interfaces.ServiceInterface;
using Milles_Project1Library.Menus;

namespace Milles_Project1Library.Factory
{
    public class GameMenuFactory : IMenuFactory
    {
        private readonly IGameService _gameService;

        public GameMenuFactory(IGameService gameService)
        {
            _gameService = gameService;
        }

        public IMenu CreateMenu()
        {
            return new GameMenu(_gameService);
        }
    }
}
