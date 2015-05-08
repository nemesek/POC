using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy
{
    class Program
    {
        static void Main(string[] args)
        {
            var id = Randomizer.RandomYes() ? 1 : 2;
            var controller = new OrdersController();
            controller.DoSomething(id);
        }
    }
}
