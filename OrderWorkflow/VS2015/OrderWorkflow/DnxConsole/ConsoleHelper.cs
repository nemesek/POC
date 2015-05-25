using System;

namespace DnxConsole
{
    public static class ConsoleHelper
    {

        public static void SetColors(ConsoleColor background, ConsoleColor foreground)
        {
            Console.BackgroundColor = background;
            Console.ForegroundColor = foreground;
        }

        public static void WriteWithStyle(ConsoleColor background, ConsoleColor foreground, string output)
        {
            SetColors(background, foreground);
            Console.WriteLine(output);
            Console.ResetColor();
            Console.WriteLine();
        }
    }
}
