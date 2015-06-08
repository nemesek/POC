using System;

namespace DnxConsole.Infrastructure.Utilities
{
    public static class ConsoleHelper
    {
        public static void WriteWithStyle(ConsoleColor background, ConsoleColor foreground, string output)
        {
            Console.BackgroundColor = background;
            Console.ForegroundColor = foreground;
            Console.WriteLine(output);
            Console.ResetColor();
            Console.WriteLine();
        }
    }
}
