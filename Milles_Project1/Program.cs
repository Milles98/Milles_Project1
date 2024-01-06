using Milles_Project1Library.Services;
using Autofac;
using Milles_Project1.Menus;
using Milles_Project1Library.Data;
using Milles_Project1Library.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Milles_Project1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "Project 1";

            using (var container = AutofacService.RegisteredContainers())
            {
                var dbContext = container.Resolve<ProjectDbContext>();

                var shapeContext = container.Resolve<IShapeContext>();
                var calculatorContext = container.Resolve<ICalculatorContext>();

                var app = new App(dbContext, shapeContext, calculatorContext);
                app.RunApplication();
            }
        }
    }
    //använd strategy pattern för calculator och shapes
    //använd autofac
    //singleton?
    //glöm ej redovisa i readme!
    //fundera över vilka attribut i de olika entities?
    //vilka entities?
    //vilka fler mappar/klasser?
    //fokusera på DRY !!


    //Shapes tabell

    //Attribut:
    //ShapeId
    //ShapeType
    //Area?
    //Form?
    //Omkrets?
    //Datum när beräkning gjorts

    //Calculator tabell

    //Attribut:
    //Beräkningstyp/operator
    //Tal 1?
    //Tal 2?
    //Resultat?
    //Datum när beräkning gjorts

    //RockPaperScissor tabell

    //Attribut:
    //GameId
    //PlayerMove
    //ComputerMove
    //Result
    //AverageWins
    //Datum när spel avklarats
}
