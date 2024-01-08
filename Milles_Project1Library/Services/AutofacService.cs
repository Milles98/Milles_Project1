using Milles_Project1Library.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Milles_Project1Library.StrategyContext;
using Milles_Project1Library.Interfaces;
using Milles_Project1Library.Services.ShapeStrategyService;
using Milles_Project1Library.Services.CalculatorStrategyService;
using Milles_Project1Library.Interfaces.ServiceInterface;

namespace Milles_Project1Library.Services
{
    public static class AutofacService
    {
        public static IContainer RegisteredContainers()
        {
            var builder = new ContainerBuilder();

            builder.Register(c =>
            {
                var dbContext = DbConfiguration.StartDatabase();
                return dbContext;
            }).As<ProjectDbContext>().InstancePerLifetimeScope();

            builder.RegisterType<CalculatorContext>().As<ICalculatorContext>();
            builder.RegisterType<ShapeContext>().As<IShapeContext>();

            builder.RegisterType<AdditionStrategy>().As<ICalculatorStrategy>();
            builder.RegisterType<DivisionStrategy>().As<ICalculatorStrategy>();
            builder.RegisterType<ModulusStrategy>().As<ICalculatorStrategy>();
            builder.RegisterType<MultiplicationStrategy>().As<ICalculatorStrategy>();
            builder.RegisterType<PowerOfStrategy>().As<ICalculatorStrategy>();
            builder.RegisterType<SubtractionStrategy>().As<ICalculatorStrategy>();

            builder.RegisterType<RectangleStrategy>().As<IShapeStrategy>();
            builder.RegisterType<ParallelogramStrategy>().As<IShapeStrategy>();
            builder.RegisterType<TriangleStrategy>().As<IShapeStrategy>();
            builder.RegisterType<RhombusStrategy>().As<IShapeStrategy>();

            builder.RegisterType<CalculatorService>().As<ICalculatorService>();
            builder.RegisterType<ShapeService>().As<IShapeService>();
            builder.RegisterType<UserHistoryService>().As<IUserHistoryService>();
            builder.RegisterType<GameService>().As<IGameService>();

            return builder.Build();
        }
    }
}
