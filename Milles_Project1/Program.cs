using Milles_Project1Library.Services;

namespace Milles_Project1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "Project 1";

            var container = AutofacService.RegisteredContainers();
            var app = new App(container);
            app.RunApplication();
        }
    }
}
