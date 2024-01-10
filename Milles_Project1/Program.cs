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
