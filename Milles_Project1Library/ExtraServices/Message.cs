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

        public static void InitialMessage()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("╭──────────────────────────────────────────────────╮");
            Console.WriteLine("│Initial information                               │");
            Console.WriteLine("│                                                  │");
            Console.WriteLine("│Welcome to Milles Project 1                       │");
            Console.WriteLine("│                                                  │");
            Console.WriteLine("│Note regarding Calculation part:                  │");
            Console.WriteLine("│I have decided to use the order                   │");
            Console.WriteLine("│choose operator and then input                    │");
            Console.WriteLine("│numbers to calculate.                             │");
            Console.WriteLine("│                                                  │");
            Console.WriteLine("│(Instead of input two numbers                     │");
            Console.WriteLine("│and then choose operator)                         │");
            Console.WriteLine("│I talked with Marcus Brederfält about this        │");
            Console.WriteLine("│and he agreed it was okay, so you know aswell!    │");
            Console.WriteLine("╰──────────────────────────────────────────────────╯");
            Console.WriteLine("");
            Console.WriteLine("╭──────────────────────────────────────────────────╮");
            Console.WriteLine("│Other than that, this app is best viewed with     │");
            Console.WriteLine("│fullscreen! enjoy and have fun :D                 │");
            Console.WriteLine("╰──────────────────────────────────────────────────╯");
            Console.ResetColor();

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
    }
}
