using Milles_Project1Library.Data;
using System;
using System.Collections.Generic;
//using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

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

            return builder.Build();
        }
    }
}
