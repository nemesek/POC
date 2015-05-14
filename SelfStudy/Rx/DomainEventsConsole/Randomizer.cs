using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEventsConsole
{
    public class Randomizer
    {
        public static bool RandomYes()
        {
            var random = new Random();
            var val = random.Next(1, 25);
            return val%2 == 0;
        }
    }
}
