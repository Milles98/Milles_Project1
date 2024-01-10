﻿namespace Milles_Project1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "Project 1";

            var container = Milles_Project1Library.Autofac.RegisteredContainers();
            var app = new App(container);
            app.RunApplication();
        }
    }
}
