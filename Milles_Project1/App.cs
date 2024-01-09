using Autofac;
using Milles_Project1.Menus;
using Milles_Project1Library.Data;
using Milles_Project1Library.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Milles_Project1Library.Interfaces.ServiceInterface;
using Milles_Project1Library.Interfaces.ContextInterface;

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
                var dbContext = scope.Resolve<ProjectDbContext>();
                var shapeContext = scope.Resolve<IShapeContext>();
                var calculatorContext = scope.Resolve<ICalculatorContext>();
                var calculatorService = scope.Resolve<ICalculatorService>();
                var shapeService = scope.Resolve<IShapeService>();
                var userHistoryService = scope.Resolve<IUserHistoryService>();
                var gameService = scope.Resolve<IGameService>();
                var dataSeeding = scope.Resolve<DataSeeding>();

                while (true)
                {
                    dataSeeding.Seed();
                    MainMenu.ShowMenu(dbContext, shapeContext, calculatorContext, calculatorService, shapeService, userHistoryService, gameService);
                }
            }
        }
    }
}
