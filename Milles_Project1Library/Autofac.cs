﻿using Milles_Project1Library.Data;
using Autofac;
using Milles_Project1Library.StrategyContext;
using Milles_Project1Library.Services.ShapeStrategyService;
using Milles_Project1Library.Services.CalculatorStrategyService;
using Milles_Project1Library.Interfaces.ServiceInterface;
using Milles_Project1Library.Interfaces.StrategyInterface;
using Milles_Project1Library.Interfaces.ContextInterface;
using Milles_Project1Library.Factory;
using Milles_Project1Library.Interfaces.FactoryInterface;
using Milles_Project1Library.FactoryMenus;
using Milles_Project1Library.Services;
using Milles_Project1Library.Interfaces;

namespace Milles_Project1Library
{
    public static class Autofac
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

            builder.RegisterType<Addition>().As<ICalculatorStrategy>();
            builder.RegisterType<Division>().As<ICalculatorStrategy>();
            builder.RegisterType<Modulus>().As<ICalculatorStrategy>();
            builder.RegisterType<Multiplication>().As<ICalculatorStrategy>();
            builder.RegisterType<SquareRoot>().As<ICalculatorStrategy>();
            builder.RegisterType<Subtraction>().As<ICalculatorStrategy>();

            builder.RegisterType<Rectangle>().As<IShapeStrategy>();
            builder.RegisterType<Parallelogram>().As<IShapeStrategy>();
            builder.RegisterType<Triangle>().As<IShapeStrategy>();
            builder.RegisterType<Rhombus>().As<IShapeStrategy>();

            builder.RegisterType<CalculatorService>().As<ICalculatorService>();
            builder.RegisterType<ShapeService>().As<IShapeService>();
            builder.RegisterType<GameService>().As<IGameService>();
            builder.RegisterType<DataSeeding>().As<IDataSeeding>();

            builder.RegisterType<CalculatorMenuFactory>().Named<IMenuFactory>("CalculatorMenuFactory").SingleInstance();
            builder.RegisterType<GameMenuFactory>().Named<IMenuFactory>("GameMenuFactory").SingleInstance();
            builder.RegisterType<ShapeMenuFactory>().Named<IMenuFactory>("ShapeMenuFactory").SingleInstance();

            return builder.Build();
        }
    }
}
