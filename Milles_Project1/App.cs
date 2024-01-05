using Autofac;
using Milles_Project1.Menus;
using Milles_Project1Library.Data;
using Milles_Project1Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milles_Project1
{
    public class App
    {
        public void Run()
        {
            using (var container = AutofacService.RegisteredContainers())
            {
                var dbContext = container.Resolve<ProjectDbContext>();

                while (true)
                {
                    MainMenu.ShowMenu();
                }
            }
        }
    }
}
