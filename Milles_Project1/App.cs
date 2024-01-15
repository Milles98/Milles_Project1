using Autofac;
using Milles_Project1Library.Data;
using Milles_Project1Library.ExtraServices;
using Milles_Project1Library.Interfaces;
using Milles_Project1Library.Menus;

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
            var dataSeeding = _container.Resolve<IDataSeeding>();

            dataSeeding.Seed();

            Message.InitialMessage();

            while (true)
            {
                MainMenu.ShowMenu(_container);
            }
        }
    }
}
