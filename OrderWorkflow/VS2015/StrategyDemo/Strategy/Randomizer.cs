using System;
using System.Threading;

namespace Strategy
{
    public class Randomizer
    {
        public static bool RandomYes()
        {
            Thread.Sleep(100); // helps with the randomization
            var random = new Random();
            return random.Next(1, 100) % 2 != 0;
        }

        public static int GetRandomFromRange(int inclusiveMin, int exclusiveMax)
        {
            Thread.Sleep(100);
            var random = new Random();
            return random.Next(inclusiveMin, exclusiveMax);
        }
    }
}
