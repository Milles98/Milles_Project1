namespace Milles_Project1Library.ExtraServices
{
    public static class Message
    {
        public static void RedMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void GreenMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void DarkYellowMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void ProjectMessage()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(" ____            _           _     _ ");
            Console.WriteLine("|  _ \\ _ __ ___ (_) ___  ___| |_  / |");
            Console.WriteLine("| |_) | '__/ _ \\| |/ _ \\/ __| __| | |");
            Console.WriteLine("|  __/| | | (_) | |  __/ (__| |_  | |");
            Console.WriteLine("|_|   |_|  \\___// |\\___|\\___|\\__| |_|");
            Console.WriteLine("              |__/                  ");
            Console.ResetColor();
        }
    }
}
