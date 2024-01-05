namespace Milles_Project1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "Project 1";

            var app = new App();
            app.Run();

            //använd strategy pattern för calculator och shapes
            //använd autofac
            //singleton?
            //glöm ej redovisa i readme!
            //fundera över vilka attribut i de olika entities?
            //vilka entities?
            //vilka fler mappar/klasser?
            //fokusera på DRY !!
        }
    }
}
