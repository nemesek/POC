using System;

namespace OrderWorkflow
{
    public static class ConsoleHelper
    {
        public static Action ResetAction { get; set; }

        public static void ResetColors()
        {
            ResetAction();
        }

        public static void SetColors(ConsoleColor background, ConsoleColor foreground)
        {
            Console.BackgroundColor = background;
            Console.ForegroundColor = foreground;
        }

        public static void WriteWithStyle(ConsoleColor background, ConsoleColor foreground, string output)
        {
            SetColors(background, foreground);
            Console.WriteLine(output);
            ResetAction();
        }
    }
}
