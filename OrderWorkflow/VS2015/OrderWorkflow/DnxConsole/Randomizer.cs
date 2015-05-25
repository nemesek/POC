using System;
using System.Threading;

namespace DnxConsole
{
    public class Randomizer
    {
        public static bool RandomYes()
        {
            Thread.Sleep(100); // helps with the randomization
            var random = new Random();
            return random.Next(1, 100)%2 != 0;
        }
    }
}
